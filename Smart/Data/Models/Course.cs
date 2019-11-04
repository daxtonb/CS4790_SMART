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
        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        public virtual ICollection<Class> Classes { get; set; }
    }
}
