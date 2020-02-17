using Core.Queries;
using CommandManagement.Domains.RBACs.ValueObjects;
using CommandManagement.Domains.RBACs.Views;

namespace CommandManagement.Domains.RBACs.Queries
{
    public class GetAuthenticater: IQuery<UserView>
    {
        public string Username { get; }
        public string Password { get; }
        public GetAuthenticater(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
