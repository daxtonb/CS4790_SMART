using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data.Models
{
    [Table(nameof(Error))]
    public class Error
    {
        public int ErrorId { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime DateTime { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
    }
}
