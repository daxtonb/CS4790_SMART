using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smart.Data.Models
{
    [Table(nameof(StudentAssessment))]
    public class StudentAssessment
    {
        public int AssessmentId { get; set; }
        public int StudentId { get; set; }
        public int PointsAwarded { get; set; }
        public string Comments { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime SubmissionDateTime { get; set; }
        public int? FileId { get; set; }

        public virtual Assessment Assessment { get; set; }
        public virtual Student Student { get; set; }
        public virtual File File { get; set; }
    }
}