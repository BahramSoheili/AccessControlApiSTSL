using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Queries;
using Core.Storage;
using DevicesSearch.RBACs.Users.Queries;
using DevicesSearch.RBACs.Users.SearchObjects;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
namespace DevicesSearch.RBACs.Users
{
    internal class UserQueryHandler :
        IQueryHandler<SearchUsers, IReadOnlyCollection<User>>,
        IQueryHandler<SearchUserById, User>,
        IQueryHandler<AuthenticateUser, User>
    {
        private readonly IRepository<User> repository;
        //private readonly AppSettings _appSettings;


        public UserQueryHandler(
           Nest.IElasticClient elasticClient,
           IRepository<User> repository
           //,IOptions<AppSettings> appSettings
        )
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<IReadOnlyCollection<User>> Handle(SearchUsers query, CancellationToken cancellationToken)
        {
            var response = await repository.Filter(query.Filter, cancellationToken);
            return response;
        }
        public async Task<User> Handle(SearchUserById request, CancellationToken cancellationToken)
        {
            var response = await repository.Find(request.Id, cancellationToken);
            return response;
        }
        public async Task<User> Handle(AuthenticateUser request, CancellationToken cancellationToken)
        {
           return await repository.Authenticate(request.Username, request.Password, cancellationToken);
            //try
            //{
            //    var user = await repository.Authenticate(request.Username, request.Password, cancellationToken);
            //    if (user != null)
            //    {
            //        var tokenHandler = new JwtSecurityTokenHandler();
            //        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            //        var tokenDescriptor = new SecurityTokenDescriptor
            //        {
            //            Subject = new ClaimsIdentity(new Claim[]
            //            {
            //            new Claim(ClaimTypes.Name, user.Id.ToString()),
            //            new Claim(ClaimTypes.Role, user.Roles.Count.ToString())
            //            }),
            //            Expires = DateTime.UtcNow.AddDays(7),
            //            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //        };
            //        var token = tokenHandler.CreateToken(tokenDescriptor);
            //        user.Data.token = tokenHandler.WriteToken(token);
            //        user.Data.password = null;
            //        //return user.WithoutPassword();
            //    }
            //    return user;
            //}
            //catch (Exception ex)
            //{
            //    throw;
            //}
        }
    }
}