using CommandManagement.Domains.RBACs.ValueObjects;
using System;

namespace CommandManagement.Domains.RBACs.Views
{
    public class UserView
    {
        public Guid Id { get; set; }
        public UserInfo Data { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public UserRole Role { get; set; }
    }
}
