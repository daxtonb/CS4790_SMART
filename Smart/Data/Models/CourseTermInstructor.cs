using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data.Models
{
    [Table(nameof(CourseTermInstructor))]
    public class CourseTermInstructor
    {
        public int CourseTermId { get; set; }
        public int UserId { get; set; }

        public virtual CourseTerm CourseTerm { get; set; }
        public virtual User User { get; set; }
    }
}
