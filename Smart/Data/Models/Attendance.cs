using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data.Models
{
    [Table(nameof(Attendance))]
    public class Attendance
    {
        public int StudentId { get; set; }
        public int CourseTermId { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public int UserId { get; set; }

        public virtual Student Student { get; set; }
        public virtual CourseTerm CourseTerm { get; set; }
        public virtual User User { get; set; }
    }
}
