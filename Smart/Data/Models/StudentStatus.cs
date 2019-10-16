using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data.Models
{
    [Table(nameof(StudentStatus))]
    public class StudentStatus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]   // We'll assign the primary key in the seeder
        public StudentStatusEnum StudentStatusId { get; set; }
        [Required]
        [MaxLength(32)]
        public string Description { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }

    public enum StudentStatusEnum
    {
        [DisplayName("Applicant")] Applicant = 1,
        [DisplayName("Waitlisted")] Waitlisted = 2,
        [DisplayName("Active")] Active = 3,
        [DisplayName("Graduated")] Graduated = 4,
        [DisplayName("Dropped")] Dropped = 5
    }
}
