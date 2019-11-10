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

        public virtual ICollection<ClassSchedule> ClassSchedules { get; set; }
        public virtual ICollection<PublicSchoolClassSchedule> PublicSchoolClassSchedules { get; set; }
    }
}