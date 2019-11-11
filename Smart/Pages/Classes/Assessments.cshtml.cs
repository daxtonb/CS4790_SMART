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
    public class AssessmentsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IEnumerable<Assessment> Assessments { get; set; }

        public AssessmentsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(int classId)
        {
            var @class = await _context.Classes
                .Include(c => c.Assessments).ThenInclude(a => a.StudentAssessments)
                .Include(c => c.Course)
                .Include(c => c.Term)
                .Include(c => c.ClassSchedules).ThenInclude(c => c.ScheduleAvailability)
                .FirstOrDefaultAsync(c => c.ClassId == classId);

            Assessments = @class.Assessments;

            ViewData["ClassTitle"] = $"{@class.Course.Name} - {@class.Term.TimeOfYear} {@class.Term.StartDate.Year}";
            ViewData["ClassSubtitle"] = ClassSchedule.GetScheduleString(@class.ClassSchedules.OrderBy(c => c.ScheduleAvailability.DayOfWeek));
            ViewData["ClassId"] = @class.ClassId;]
        }
    }
}