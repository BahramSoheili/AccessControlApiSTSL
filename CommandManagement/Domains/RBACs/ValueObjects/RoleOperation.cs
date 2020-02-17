
using System;

namespace CommandManagement.Domains.RBACs.ValueObjects
{
    public class RoleOperation
    {
        public Guid OperationId { get; set; }
        public string OperationName { get; set; }
        public string description { get; set; }
    }
}

