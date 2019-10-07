using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data.Models
{
    [Table(nameof(PublicSchoolCourseSchedule))]
    public class PublicSchoolCourseSchedule
    {
        public int PublicSchoolCourseScheduleId { get; set; }
        public int StudentPublicSchoolCourseId { get; set; }
        public int ScheduleId { get; set; }

        public virtual StudentPublicSchoolCourse StudentPublicSchoolCourse { get; set; }
        public virtual Schedule Schedule { get; set; }
    }
}
