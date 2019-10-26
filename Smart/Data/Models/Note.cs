using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data.Models
{
    [Table(nameof(Note))]
    public class Note
    {
        public int NoteId { get; set; }
        public int Studentid { get; set; }
        public int UserId { get; set; }
        public int NoteTypeId { get; set; }
        public string Text { get; set; }

        public virtual Student Student { get; set; }
        public virtual User User { get; set; }
        public virtual NoteType NoteType { get; set; }
    }
}
