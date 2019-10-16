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
        public int ClassId { get; set; }
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        [Column(TypeName = "time(0)")]
        public TimeSpan? TimeIn { get; set; }
        [Column(TypeName = "time(0)")]
        public TimeSpan? TimeOut { get; set; }
        public AttendanceStatusEnum AttendanceStatusId { get; set; }

        public virtual Student Student { get; set; }
        public virtual Class Class { get; set; }
        public AttendanceStatus AttendanceStatus { get; set; }
    }
}
