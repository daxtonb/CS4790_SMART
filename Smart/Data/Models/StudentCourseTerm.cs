using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data.Models
{
    [Table(nameof(StudentClass))]
    public class StudentClass
    {
        public int StudentId { get; set; }
        public int ClassId { get; set; }

        public virtual Student Student { get; set; }
        public virtual Class Class { get; set; }
    }
}
