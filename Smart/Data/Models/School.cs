using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data.Models
{
    [Table(nameof(School))]
    public class School
    {
        public int SchoolId { get; set; }
        [MaxLength(128)]
        public string Name { get; set; }

        public virtual ICollection<ScheduleAvailability> ScheduleAvailabilities { get; set; }
    }
}
