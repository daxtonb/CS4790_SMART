using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smart.Data.Models
{
    [Table(nameof(FileType))]
    public class FileType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]   // We'll assign the primary key in the seeder
        public FileTypeEnum FileTypeId { get; set; }
        [Required]
        [MaxLength(128)]
        public string Description { get; set; }

        public virtual ICollection<File> Files { get; set; }
    }

    public enum FileTypeEnum { 
        [DisplayName("Assessment")] Assessment = 1, 
        [DisplayName("Personal Document")] PersonalDocument = 2,
        [DisplayName("Medical Document")] MedicalDocument = 3,
        [DisplayName("Certificate")] Certificate = 4,
        [DisplayName("Other")] Other = 5
    }
}