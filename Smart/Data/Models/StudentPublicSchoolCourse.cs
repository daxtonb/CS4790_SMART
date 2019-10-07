using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smart.Data.Models
{
    [Table(nameof(StudentPublicSchoolCourse))]
    public class StudentPublicSchoolCourse
    {
        public int StudentPublicSchoolCourseId { get; set; }
        public int StudentId { get; set; }
        [Required]
        [MaxLength(128)]
        public string CourseName { get; set; }
        public TimeOfYear TimeOfYear { get; set; }

        public virtual ICollection<PublicSchoolCourseSchedule> PublicSchoolCourseSchedules { get; set; }
        public virtual Student Student { get; set; }
    }
}