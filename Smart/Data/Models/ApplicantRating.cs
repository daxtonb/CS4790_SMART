using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data.Models
{
    [Table(nameof(ApplicantRating))]
    public class ApplicantRating
    {
        public int ApplicantRatingId { get; set; }
        public int StudentId { get; set; }
        public int UserId { get; set; }
        public int RatingCiteriumId { get; set; }
        public int TermId { get; set; }
        public int ScoreAssigned { get; set; }

        public virtual Student Student { get; set; }
        public virtual User User { get; set; }
        public virtual RatingCirterium RatingCirterium { get; set; }
        public virtual Term Term { get; set; }
    }
}
