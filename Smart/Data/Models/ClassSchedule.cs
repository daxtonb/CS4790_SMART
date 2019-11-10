using Smart.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data.Models
{
    [Table(nameof(ClassSchedule))]
    public class ClassSchedule
    {
        public int ClassId { get; set; }
        public int ScheduleAvailabilityId { get; set; }

        public virtual Class Class { get; set; }
        public virtual ScheduleAvailability ScheduleAvailability { get; set; }
        public static string GetScheduleString(IEnumerable<ClassSchedule> classSchedules)
        {
            string dayOfWeek, timeRange, workignString = string.Empty;
            ScheduleAvailability current, next = null;
            var arr = classSchedules.ToArray();

            for (int i = 0; i < arr.Length; i++)
            {
                current = arr[i].ScheduleAvailability;
                next = i < arr.Length - 1 ? arr[i + 1].ScheduleAvailability : null;
                workignString += GetDayOfWeekAbbreviation(current.DayOfWeek);

                // CONDITION: This is the last element or the next element has a different time schedule
                if (i == arr.Length - 1 || (next != null && (next.StartTime != current.StartTime || next.EndTime != current.EndTime)))
                {
                    workignString += " " + current.StartTime.ToString12HourTime() + "-" + current.EndTime.ToString12HourTime() + ", ";
                }
            }

            workignString = workignString.Trim().TrimEnd(',');

            return workignString;
        }

        private static string GetDayOfWeekAbbreviation(DayOfWeek dayOfWeek)
        {
            if (dayOfWeek == DayOfWeek.Tuesday || dayOfWeek == DayOfWeek.Thursday)
            {
                return dayOfWeek.ToString().Substring(0, 2);
            }

            return dayOfWeek.ToString().Substring(0, 1);
        }
    }
}
