using System;
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
        public TimeOfYear TimeOfYear { get; set; }

        public virtual ICollection<Class> Classes { get; set; }

        public override string ToString()
        {
            return TimeOfYear + " " + StartDate.Year;
        }

        public static TimeOfYear GetTimeOfYear(DateTime dateTime)
        {
            if (dateTime.Month >= 1 && dateTime.Month < 5)
                return TimeOfYear.Spring;
            if (dateTime.Month >= 5 && dateTime.Month < 9)
                return TimeOfYear.Summer;
            return TimeOfYear.Fall;
        }

        public static DateTime GetStartDate(TimeOfYear timeOfYear, int year)
        {
            switch (timeOfYear)
            {
                case TimeOfYear.Spring:
                    return new DateTime(year, 1, 1);
                case TimeOfYear.Summer:
                    return new DateTime(year, 5, 1);
                case TimeOfYear.Fall:
                    return new DateTime(year, 9, 1);
                default:
                    throw new Exception("Invalid time of year");
            }
        }
        public static DateTime GetEndDate(TimeOfYear timeOfYear, int year)
        {
            if (timeOfYear == TimeOfYear.Fall)
                return GetStartDate(TimeOfYear.Spring, year + 1).AddDays(-1);
            else
                return GetStartDate(timeOfYear + 1, year).AddDays(-1);
        }
    }
}
