using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Extensions
{
    public static class TimeSpanExtensions
    {
        /// <summary>
        /// Returns string time in h:mm tt format.
        /// </summary>
        public static string ToString12HourTime(this TimeSpan timeSpan)
        {

            string amPm = "AM";
            if (timeSpan.Hours >= 12)
            {
                amPm = "PM";
                if (timeSpan.Hours > 12)
                {
                    timeSpan = new TimeSpan(timeSpan.Hours - 12, timeSpan.Minutes, 0);
                }
            }
            else if (timeSpan.Hours == 0)
            {
                timeSpan = new TimeSpan(12, timeSpan.Minutes, 0);
            }

            return $"{timeSpan.ToString(@"h\:mm")} {amPm}";
        }
    }
}
