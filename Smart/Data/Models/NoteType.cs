using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data.Models
{
    [Table(nameof(NoteType))]
    public class NoteType
    {
        public int NoteTypeId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Note> Notes { get; set; }
    }
}
