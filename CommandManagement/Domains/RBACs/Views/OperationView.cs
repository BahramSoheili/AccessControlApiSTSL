using CommandManagement.Domains.RBACs.ValueObjects;
using System;
namespace CommandManagement.Domains.RBACs.Views
{
    public class OperationView
    {
        public Guid Id { get; set; }
        public OperationInfo Data { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }
}
