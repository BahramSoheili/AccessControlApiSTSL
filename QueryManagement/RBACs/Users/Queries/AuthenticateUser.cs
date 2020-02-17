using Core.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevicesSearch.RBACs.Users.Queries
{
    public class AuthenticateUser: IQuery<User>
    {
        public string Username { get; }
        public string Password { get; }
        public AuthenticateUser(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
