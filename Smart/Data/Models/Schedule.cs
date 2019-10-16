using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smart.Data.Models
{
    [Table(nameof(Schedule))]
    public class Schedule
    {
        public int ScheduleId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        [Column(TypeName = "time(0)")]
        public TimeSpan StartTime { get; set; }
        [Column(TypeName = "time(0)")]
        public TimeSpan EndTime { get; set; }

        public virtual ICollection<ClassSchedule> ClassSchedules { get; set; }
        public virtual ICollection<PublicSchoolClassSchedule> PublicSchoolClassSchedules { get; set; }
    }
}