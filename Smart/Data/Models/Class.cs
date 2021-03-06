﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Smart.Data.Models
{
    [Table(nameof(Class))]
    public class Class
    {
        public int ClassId { get; set; }
        public int CourseId { get; set; }
        [Display(Name = "Term")]
        public int TermId { get; set; }
        [Display(Name = "Instructor")]
        public int InstructorUserId { get; set; }
        [Range(1,255)]
        public byte Capacity { get; set; }
        [Range(0,100)]
        [Display(Name = "Passing Grade Threshold")]
        public double PassingGradeThreshold { get; set; }

        public virtual Course Course { get; set; }
        public virtual Term Term { get; set; }
        public virtual User InstructorUser { get; set; }
        public virtual ICollection<Meeting> Meetings { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<Assessment> Assessments { get; set; }

    }
}
