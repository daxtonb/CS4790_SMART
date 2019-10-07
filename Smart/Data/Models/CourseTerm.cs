using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data.Models
{
    [Table(nameof(CourseTerm))]
    public class CourseTerm
    {
        public int CourseTermId { get; set; }
        public int CourseId { get; set; }
        public int TermId { get; set; }
        public byte Capacity { get; set; }


        public virtual Course Course { get; set; }
        public virtual Term Term { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<CourseTermSchedule> CourseTermSchedules { get; set; }
        public virtual ICollection<CourseTermInstructor> CourseTermInstructors { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }

    }
}
