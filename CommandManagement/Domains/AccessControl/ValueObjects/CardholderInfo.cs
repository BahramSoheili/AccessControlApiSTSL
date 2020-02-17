using System;
using System.Collections.Generic;
using System.Text;

namespace CommandManagement.Domains.AccessControl.ValueObjects
{
    public class CardholderInfo
    {
        public string cardholderFirstname { get; set; }
        public string cardholderLastname { get; set; }
        public string devision { get; set; }
        public string email { get; set; }
        public string birthDate { get; set; }
        public string mobile { get; set; }
        public string description { get; set; }
    }
}
