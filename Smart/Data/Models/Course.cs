using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data.Models
{
    [Table(nameof(Course))]
    public class Course
    {
        public int CourseId { get; set; }
        [MaxLength(128)]
        public string Name { get; set; }
        public int Number { get; set; }

        public virtual ICollection<CourseTerm> CourseTerms { get; set; }
    }
}
