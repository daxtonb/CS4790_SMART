using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data.Models
{
    [Table(nameof(StudentCourseTerm))]
    public class StudentCourseTerm
    {
        public int StudentId { get; set; }
        public int CourseTermId { get; set; }

        public virtual Student Student { get; set; }
        public virtual CourseTerm CourseTerm { get; set; }
    }
}
