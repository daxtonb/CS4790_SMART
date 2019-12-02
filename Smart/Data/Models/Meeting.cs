using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System;
using Smart.Extensions;

namespace Smart.Data.Models
{
    [Table(nameof(Meeting))]
    public class Meeting
    {
        public int MeetingId { get; set; }
        public int ClassId { get; set; }
        public int ScheduleAvailabilityId { get; set; }
        public int MeetingOrderNum { get; set; }

        public virtual Class Class { get; set; }
        public virtual ScheduleAvailability ScheduleAvailability { get; set; }
        public virtual ICollection<StudentMeeting> StudentMeetings { get; set; }

        public static string GetScheduleString(IEnumerable<Meeting> classSchedules)
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
            if (dayOfWeek == DayOfWeek.Monday || dayOfWeek == DayOfWeek.Wednesday || dayOfWeek == DayOfWeek.Friday)
            {
                return dayOfWeek.ToString().Substring(0, 1);
            }
            return dayOfWeek.ToString().Substring(0, 2);
        }
    }
}