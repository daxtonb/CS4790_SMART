using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smart.Data.Models
{
    [Table(nameof(ScheduleAvailability))]
    public class ScheduleAvailability
    {
        public int ScheduleAvailabilityId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        [Column(TypeName = "time(0)")]
        public TimeSpan StartTime { get; set; }
        [Column(TypeName = "time(0)")]
        public TimeSpan EndTime { get; set; }

        public virtual ICollection<ClassSchedule> ClassSchedules { get; set; }
        public virtual ICollection<PublicSchoolClassSchedule> PublicSchoolClassSchedules { get; set; }
    }
}