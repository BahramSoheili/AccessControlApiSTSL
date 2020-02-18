using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeroManagement
{
    public class scheduleTable
    {
        public List<schedule> scheduleList { get; set; }
    }
    public class schedule
    {
        public string name { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public bool[] tz1_days = new bool[7];

        public schedule()
        {
            name = null;
            startTime = null;
            endTime = null;
            for (int i = 0; i < Constants.DAYS_OF_WEEK; i++)
            {
                tz1_days[i] = false;
            }
        }
        public schedule(string tname, string tstime, string tetime, List<bool> days)
        {
            name = tname;
            startTime = tstime;
            endTime = tetime;

            for (int i = 0; i < Constants.DAYS_OF_WEEK; i++)
            {
                tz1_days[i] = days[i];
            }
        }

    }
}
