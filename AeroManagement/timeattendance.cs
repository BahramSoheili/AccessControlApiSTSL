using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeroManagement
{
    public class timeattendance
    {
        public string startTime { get; set; }
        public string endTime { get; set; }
        public DateTime startTimeInterval { get; set; }
        public DateTime endTimeInterval { get; set; }
        public TimeSpan workingHours { get; set; }
        public TimeSpan FirstToLast { get; set; }
        public bool inOffice { get; set; }
        public List<string> inRecords { get; set; }

        public timeattendance()
        {
            startTime = null;
            endTime = null;
            startTimeInterval = DateTime.MinValue;
            endTimeInterval = DateTime.MinValue;
            workingHours = TimeSpan.Zero;
            FirstToLast = TimeSpan.Zero;
            inOffice = false;
            inRecords = new List<string>();
        }
    }
}
