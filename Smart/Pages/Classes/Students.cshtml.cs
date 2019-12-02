using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Smart.Data;
using Smart.Data.Models;

namespace Smart.Pages.Classes
{
    public class StudentListModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IEnumerable<StudentViewModel> Students { get; set; }

        public StudentListModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(int classId)
        {
            var @class = await _context.Classes
                .Include(c => c.Assessments).ThenInclude(a => a.StudentAssessments)
                .Include(c => c.Attendances)
                .Include(c => c.Course)
                .Include(c => c.Term)
                .Include(c => c.Meetings).ThenInclude(m => m.ScheduleAvailability)
                .Include(c => c.Meetings).ThenInclude(m => m.StudentMeetings)
                .FirstOrDefaultAsync(c => c.ClassId == classId);

            double? assessmentPointsPossible = @class.Assessments.Count > 0 ? @class.Assessments.Sum(a => a.PointsPossible) : (int?)null;
            double? attendanceDays = @class.Attendances.Count > 0 ? @class.Attendances.Select(a => a.Date.Date).Distinct().Count() : (int?)null;

            Students = @class.Meetings.SelectMany(m => m.StudentMeetings.Select(s => s.Student)).Distinct().Select(s => new StudentViewModel
            {
                StudentId = s.StudentId,
                Name = $"{s.LastName}, {s.FirstName}",
                GradeAverage = GetRoundedPercent(s.StudentAssessments?.Where(k => k.Assessment != null).Sum(a => a.PointsAwarded), assessmentPointsPossible),
                AttendanceAverage = GetRoundedPercent(s.Attendances?.Where(k => k.Meeting.ClassId == classId).Count(a => a.AttendanceStatusId != AttendanceStatusEnum.Absent), attendanceDays)
            });

            // For layout
            ViewData["ClassId"] = @class.ClassId;
            ViewData["ClassTitle"] = $"{@class.Course.Name} - {@class.Term.Name}";
            ViewData["ClassSubtitle"] = Meeting.GetScheduleString(@class.Meetings.OrderBy(c => c.ScheduleAvailability.DayOfWeek));
        }

        private double? GetRoundedPercent(double? value, double? total)
        {
            if (value.HasValue && total.HasValue && total > 0)
                return Math.Round((value.Value / total.Value) * 100, 2);
            else
                return null;
        }

        public class StudentViewModel
        {
            public int StudentId { get; set; }
            public string Name { get; set; }
            public double? GradeAverage { get; set; }
            public double? AttendanceAverage { get; set; }
        }
    }
}