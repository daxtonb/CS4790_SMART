﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data.Models
{
    [Table(nameof(Term))]
    public class Term
    {
        public int TermId { get; set; }
        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }
        [Required, MaxLength(128)]
        public string Name { get; set; }

        public virtual ICollection<Class> Classes { get; set; }
        public virtual ICollection<ApplicantRating> ApplicantRatings { get; set; }
    }
}
