using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeroManagement
{
    public class CardUsers
    {
        public List<cardUser> CardUserList { get; set; }
    }
    public class cardUser
    {
        public string cardNum { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string pin { get; set; }
        public string accesslevel { get; set; }
        public string schedule { get; set; }
        public cardUser()
        {
            firstName = null;
            lastName = null;
            cardNum = null;
            pin = null;
            accesslevel = null;
            schedule = null;
        }
        public cardUser(string cn, string fname, string lname, string cpin, string al, string tz)
        {
            cardNum = cn;
            firstName = fname;
            lastName = lname;
            pin = cpin;
            accesslevel = al;
            schedule = tz;
        }

    }

}
