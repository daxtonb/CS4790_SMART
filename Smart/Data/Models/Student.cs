using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data.Models
{
    [Table(nameof(Student))]
    public class Student
    {
        public int StudentId { get; set; }
        [Required]
        [MaxLength(32)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(32)]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }
        [MaxLength(128)]
        public string Address { get; set; }
        [MaxLength(64)]
        public string Village { get; set; }
        public double LocationLattitude { get; set; }
        public double LocationLongitude { get; set; }
        public byte PublicSchoolLevel { get; set; }
        [MaxLength(64)]
        public string GuardianName { get; set; }
        [MaxLength(16)]
        public string Phone { get; set; }
        [MaxLength(256)]
        public string Photo { get; set; }
        public StudentStatusEnum StudentStatusId { get; set; }


        public virtual StudentStatus StudentStatus { get; set; }

        public virtual ICollection<ApplicantRating> ApplicantRatings { get; set; }
        public virtual ICollection<StudentClass> StudentClasses { get; set; }
        public virtual ICollection<StudentPublicSchoolClass> StudentPublicSchoolClasss { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<StudentAssessment> StudentAssessments { get; set; }
        public virtual ICollection<File> Files { get; set; }
    }
}
