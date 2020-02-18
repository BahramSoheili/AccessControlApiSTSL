using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AeroApi.Helpers
{
    public static class Helper
    {
        public static string GetTimeStamp()
        {
            DateTime now = DateTime.Now;
            return now.ToString("g", DateTimeFormatInfo.InvariantInfo);
        }
    }
}
