using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data.Models
{
    [Table(nameof(AttendanceStatus))]
    public class AttendanceStatus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]   // We'll assign the primary key in the seeder
        public AttendanceStatusEnum AttendanceStatusId { get; set; }
        [Required]
        [MaxLength(32)]
        public string Description { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }
    }

    public enum AttendanceStatusEnum
    {
        [DisplayName("On Time")] OnTime = 1,
        [DisplayName("Late")] Late = 2,
        [DisplayName("Absent")] Absent = 3
    }
}
