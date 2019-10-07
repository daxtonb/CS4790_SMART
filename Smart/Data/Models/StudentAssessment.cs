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

        public virtual Assessment Assessment { get; set; }
        public virtual Student Student { get; set; }
    }
}