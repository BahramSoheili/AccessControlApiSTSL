using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Queries;
using DevicesSearch.RBACs.Roles.SearchObjects;
using DevicesSearch.RBACs.Users;
using DevicesSearch.RBACs.Users.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DevicesSearch.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController: Controller
    {
        private readonly IQueryBus _queryBus;
        private readonly AppSettings _appSettings;
        public UsersController(IQueryBus queryBus, IOptions<AppSettings> appSettings)
        {
            _queryBus = queryBus;
            _appSettings = appSettings.Value;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public Task<IReadOnlyCollection<User>> Search([FromQuery]string filter)
        {
            return _queryBus.Send<SearchUsers, IReadOnlyCollection<User>>(new SearchUsers(filter));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public Task<User> SearchById(Guid Id)
        {
            return _queryBus.Send<SearchUserById, User>(new SearchUserById(Id));
        }

        [AllowAnonymous]
        [HttpPost("queryAuthenticate")]
        public Task<User> queryAuthenticate([FromBody]AuthenticateModel model)
        {
            var user = _queryBus.Send<AuthenticateUser, User>(new AuthenticateUser(model.username, model.password));
            try
            {
                if (user.Result != null)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, user.Result.Id.ToString()),
                            new Claim(ClaimTypes.Role, user.Result.Role.roleName)
                        }),
                        Expires = DateTime.UtcNow.AddMinutes(10),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), 
                        SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    user.Result.Data.queryToken = tokenHandler.WriteToken(token);
                    user.Result.Data.password = null;
                    //return user.WithoutPassword();
                }
                return user;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
