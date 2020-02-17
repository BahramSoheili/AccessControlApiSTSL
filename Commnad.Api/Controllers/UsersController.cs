using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Commands;
using Core.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using CommandManagement.Domains.RBACs.Views;
using CommandManagement.Domains.RBACs.Commands;
using CommandManagement.Domains.RBACs.Queries;
using CommandManagement.Domains.RBACs.ValueObjects;

namespace CommandManagement.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;
        private readonly AppSettings _appSettings;
        public UsersController(ICommandBus commandBus, IQueryBus queryBus, IOptions<AppSettings> appSettings)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
            _appSettings = appSettings.Value;
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public Task<UserView> Get(Guid id)
        {
            return _queryBus.Send<GetUser, UserView>(new GetUser(id));
        }
        /*
        {
	        "Item1":
	        {
                "userName":"plplp",
                "password":"kooooooookokokok",
                .
                .
                .
            },
            "Item2":
            {
                "RoleId":"6b510c74-f2e3-469e-a8a4-04e8346bda50",
                "RoleName":"vffffffff",
                "description":"llllllllllllllll"
            }
        }
        */
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody](UserInfo userInfo, UserRole role) command)
        {
            var id = Guid.NewGuid();
            var user = new CreateUser(id, command.userInfo, command.role);
            await _commandBus.Send(user);
            return Created("api/users", id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody](UserInfo userInfo, UserRole role) command)
        {
            await _commandBus.Send(new UpdateUser(id, command.userInfo, command.role));
            return Ok();
        }
        [AllowAnonymous]
        [HttpPost("commandAuthenticate")]
        public async Task commandAuthenticate([FromBody]AuthenticateInfo inputData)
        {
            var claimElements =  _queryBus.Send<GetAuthenticater, UserView>(new GetAuthenticater(inputData.username, inputData.password));
            try
            {
                if (claimElements != null)
                {
                    var userId = claimElements.Result.Id;
                    var user = claimElements.Result.Data;
                    var role = claimElements.Result.Role;
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, userId.ToString()),
                            new Claim(ClaimTypes.Role, role.RoleName)
                        }),
                        Expires = DateTime.UtcNow.AddMinutes(10),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature)
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    user.commandToken = tokenHandler.WriteToken(token);
                    await _commandBus.Send(new UpdateUser(userId, user, role));
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}