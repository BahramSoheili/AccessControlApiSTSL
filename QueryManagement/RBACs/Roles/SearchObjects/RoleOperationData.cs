using System;

namespace DevicesSearch.RBACs.Roles.SearchObjects
{
    //It should be the same as RoleOperation

    public class RoleOperationData
    {
        public Guid OperationId { get; set; }
        public string OperationName { get; set; }
        public string description { get; set; }
    }
}

