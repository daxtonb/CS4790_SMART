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
        public int AttendanceId { get; set; }
        public int StudentId { get; set; }
        public int MeetingId { get; set; }
        public DateTime Date { get; set; }
        public AttendanceStatusEnum AttendanceStatusId { get; set; }
        public string Comments { get; set; }

        public virtual Student Student { get; set; }
        public virtual Meeting Meeting { get; set; }
        public AttendanceStatus AttendanceStatus { get; set; }
    }
}
