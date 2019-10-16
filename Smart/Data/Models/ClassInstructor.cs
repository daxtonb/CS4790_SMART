using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data.Models
{
    [Table(nameof(ClassInstructor))]
    public class ClassInstructor
    {
        public int ClassId { get; set; }
        public int UserId { get; set; }

        public virtual Class Class { get; set; }
        public virtual User User { get; set; }
    }
}
