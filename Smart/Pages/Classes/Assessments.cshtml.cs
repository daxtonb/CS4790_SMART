using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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

            Assessments = @class.Assessments.OrderByDescending(a => a.Deadline);

            // For layout
            ViewData["ClassTitle"] = $"{@class.Course.Name} - {@class.Term.TimeOfYear} {@class.Term.StartDate.Year}";
            ViewData["ClassSubtitle"] = ClassSchedule.GetScheduleString(@class.ClassSchedules.OrderBy(c => c.ScheduleAvailability.DayOfWeek));
            ViewData["ClassId"] = @class.ClassId;
        }

        public async Task<IActionResult> OnGetAssessmentFormAsync(int classId, int? assessmentId)
        {
            Assessment assessment;

            if (assessmentId.HasValue)
            {
                assessment = await _context.Assessments.FirstOrDefaultAsync(a => a.AssessmentId == assessmentId.Value);

                if (assessment == null)
                {
                    return NotFound();
                }
            }
            else
            {
                assessment = new Assessment() { ClassId = classId, Deadline = DateTime.Today.AddDays(1) };
            }


            return new PartialViewResult()
            {
                ViewName = "_AssessmentForm",
                ViewData = new ViewDataDictionary<Assessment>(ViewData, assessment)
            };
        }
    }
}