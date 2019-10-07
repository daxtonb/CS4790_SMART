using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Smart.Data.Models
{
    public class FileType
    {
        public int FileTypeId { get; set; }
        [Required]
        [MaxLength(128)]
        public string Description { get; set; }

        public virtual ICollection<File> Files { get; set; }
    }
}