using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data.Models
{
    [Table(nameof(Assessment))]
    public class Assessment
    {
        public int AssessmentId { get; set; }
        public int ClassId { get; set; }
        [Required]
        [MaxLength(128)]
        public string Title { get; set; }
        public string Description { get; set; }
        public int PointsPossible { get; set; }

        public virtual Class Class { get; set; }
        public virtual ICollection<StudentAssessment> StudentAssessments { get; set; }
    }
}
