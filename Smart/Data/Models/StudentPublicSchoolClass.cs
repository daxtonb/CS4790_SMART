using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smart.Data.Models
{
    [Table(nameof(StudentPublicSchoolClass))]
    public class StudentPublicSchoolClass
    {
        public int StudentPublicSchoolClassId { get; set; }
        public int StudentId { get; set; }
        [Required]
        [MaxLength(128)]
        public string CourseName { get; set; }
        public TimeOfYear TimeOfYear { get; set; }
        public int TermId { get; set; }

        public virtual ICollection<PublicSchoolClassSchedule> PublicSchoolClassSchedules { get; set; }
        public virtual Student Student { get; set; }
        public virtual Term Term { get; set; }
    }
}