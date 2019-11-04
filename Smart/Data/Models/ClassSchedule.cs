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
    }
}
