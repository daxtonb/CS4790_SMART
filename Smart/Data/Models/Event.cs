using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data.Models
{
    [Table(nameof(Event))]
    public class Event
    {
        public int EventId { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
