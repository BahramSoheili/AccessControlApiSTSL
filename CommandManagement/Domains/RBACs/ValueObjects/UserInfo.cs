using System.Collections.Generic;

namespace CommandManagement.Domains.RBACs.ValueObjects
{
    public class UserInfo
    {
        public string username { get; set; }
        public string password { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string office { get; set; }
        public string division { get; set; }
        public string department { get; set; }
        public string personnelID { get; set; }
        public string commandToken { get; set; }
        public string queryToken { get; set; }
        public string description { get; set; }
    }
}
