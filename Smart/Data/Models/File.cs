using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data.Models
{
    [Table(nameof(File))]
    public class File
    {
        public int FileId { get; set; }
        public int StudentId { get; set; }
        public int FileTypeId { get; set; }
        [Required]
        [MaxLength(256)]
        public string Path { get; set; }

        public virtual Student Student { get; set; }
        public virtual FileType FileType { get; set; }
    }
}
