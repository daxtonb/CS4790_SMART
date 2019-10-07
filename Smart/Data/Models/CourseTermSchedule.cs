using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data.Models
{
    [Table(nameof(CourseTermSchedule))]
    public class CourseTermSchedule
    {
        public int CourseTermId { get; set; }
        public int ScheduleId { get; set; }

        public virtual CourseTerm CourseTerm { get; set; }
        public virtual Schedule Schedule { get; set; }
    }
}
