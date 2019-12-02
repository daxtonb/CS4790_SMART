using Smart.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Smart.Data.Models
{
    [Table(nameof(ScheduleAvailability))]
    public class ScheduleAvailability
    {
        public int ScheduleAvailabilityId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        [DataType(DataType.Time)]
        [Column(TypeName = "time(0)")]
        public TimeSpan StartTime { get; set; }
        [DataType(DataType.Time)]
        [Column(TypeName = "time(0)")]
        public TimeSpan EndTime { get; set; }
        public int SchoolId { get; set; }

        public virtual School School { get; set; }
        public virtual ICollection<Meeting> Meetings { get; set; }

        public static string GetScheduleString(IEnumerable<ScheduleAvailability> classSchedules)
        {
            string dayOfWeek, timeRange, workignString = string.Empty;
            ScheduleAvailability current, next = null;
            var arr = classSchedules.ToArray();

            for (int i = 0; i < arr.Length; i++)
            {
                current = arr[i];
                next = i < arr.Length - 1 ? arr[i + 1] : null;
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
            if (dayOfWeek == DayOfWeek.Monday || dayOfWeek == DayOfWeek.Wednesday || dayOfWeek == DayOfWeek.Friday)
            {
                return dayOfWeek.ToString().Substring(0, 1);
            }
            return dayOfWeek.ToString().Substring(0, 2);
        }
    }
}