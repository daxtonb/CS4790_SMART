using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data.Models
{
    [Table(nameof(Class))]
    public class Class
    {
        public int ClassId { get; set; }
        public int CourseId { get; set; }
        public int TermId { get; set; }
        public byte Capacity { get; set; }


        public virtual Course Course { get; set; }
        public virtual Term Term { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<ClassSchedule> ClassSchedules { get; set; }
        public virtual ICollection<ClassInstructor> ClassInstructors { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }

    }
}
