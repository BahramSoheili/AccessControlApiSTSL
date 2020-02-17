using CommandManagement.Domains.RBACs.ValueObjects;
using System;
using System.Collections.Generic;

namespace CommandManagement.Domains.RBACs.Views
{
    public class RoleView
    {
        public Guid Id { get; set; }
        public RoleInfo Data { get; set; }        
        public DateTime Created { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public List<RoleOperation> Operations { get; set; }
    }
}
