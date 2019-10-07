using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data.Models
{
    [Table(nameof(RatingCirterium))]
    public class RatingCirterium
    {
        public int RatingCirteriumId { get; set; }
        [Required]
        [MaxLength(512)]
        public string Description { get; set; }
        public int MaxScore { get; set; }

        public virtual ICollection<ApplicantRating> ApplicantRatings { get; set; }
    }
}
