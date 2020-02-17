using System;
namespace CommandManagement.Domains.RBACs.ValueObjects
{
    //It should be the same as UserRole

    public class UserRole
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public string description { get; set; }
    }
}
