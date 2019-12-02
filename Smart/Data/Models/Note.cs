using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public string Text { get; set; }
        [Required, MaxLength(128)]
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Student Student { get; set; }
        public virtual User User { get; set; }
        public virtual NoteType NoteType { get; set; }
    }
}
