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

        public ViewModel ClassVm { get; set; }

        public StudentListModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(int classId)
        {
            var @class = await _context.Classes
                .Include(c => c.StudentClasses).ThenInclude(c => c.Student).ThenInclude(c => c.StudentAssessments)
                .Include(c => c.Assessments)
                .Include(c => c.Attendances)
                .Include(c => c.Course)
                .Include(c => c.Term)
                .Include(c => c.ClassSchedules).ThenInclude(c => c.ScheduleAvailability)
                .FirstOrDefaultAsync(c => c.ClassId == classId);

            ClassVm = new ViewModel()
            {
                ClassId = @class.ClassId,
                Title = $"{@class.Course.Name} - {@class.Term.TimeOfYear} {@class.Term.StartDate.Year}",
                Subtitle = ClassSchedule.GetScheduleString(@class.ClassSchedules.OrderBy(c => c.ScheduleAvailability.DayOfWeek)),
                AssessmentPointsPossible = @class.Assessments.Count > 0 ? @class.Assessments.Sum(a => a.PointsPossible) : (int?)null,
                AttendanceDays = @class.Attendances.Count > 0 ? @class.Attendances.Select(a => a.Date.Date).Distinct().Count() : (int?)null,
                Stuents = @class.StudentClasses.Select(s => new StudentViewModel
                {
                    StudentId = s.StudentId,
                    Name = $"{s.Student.LastName}, {s.Student.FirstName}",
                    ClassAssmentPointsTotal = s.Student.StudentAssessments?.Sum(a => a.PointsAwarded),
                    ClassAttendanceTotal = s.Student.Attendances?.Count(a => a.AttendanceStatusId != AttendanceStatusEnum.Absent)
                })
            };
        }

        public class ViewModel
        {
            public int ClassId { get; set; }
            public string Title { get; set; }
            public string Subtitle { get; set; }
            public int? AssessmentPointsPossible { get; set; }
            public int? AttendanceDays { get; set; }
            public IEnumerable<StudentViewModel> Stuents { get; set; }
        }

        public class StudentViewModel
        {
            public int StudentId { get; set; }
            public string Name { get; set; }
            public int? ClassAssmentPointsTotal { get; set; }
            public int? ClassAttendanceTotal { get; set; }
        }
    }
}