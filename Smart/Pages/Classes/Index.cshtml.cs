using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Smart.Data;
using Smart.Data.Models;
using Smart.Extensions;

namespace Smart.Pages.Classes
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CourseViewModel> Courses { get; set; }
        public IEnumerable<Term> Terms { get; set; }
        public IEnumerable<School> Schools { get; set; }
        public int? SelectedCourseId { get; set; }
        public Term SelectedTerm { get; set; }
        public int SchoolId { get; set; }

        public async Task OnGetAsync(int? schoolId, int? termId, int? courseId)
        {
            SelectedCourseId = courseId;

            // Schools
            Schools = await _context.Schools.Include(c => c.Courses).ToListAsync();
            if (schoolId == null)
            {
                SchoolId = Schools.First().SchoolId;
            }
            else
            {
                SchoolId = schoolId.Value;
            }

            // Term
            Terms = await _context.Terms.ToListAsync();
            if (termId == null)
            {
                SelectedTerm = Terms.FirstOrDefault(t => t.StartDate <= DateTime.Today && t.EndDate >= DateTime.Today) ?? Terms.First();
            }
            else
            {
                SelectedTerm = Terms.FirstOrDefault(t => t.TermId == termId) ?? Terms.First();
            }

            // Classes
            var classes = await _context.Classes
                .Include(c => c.Meetings).ThenInclude(m => m.StudentMeetings)
                .Include(c => c.Meetings).ThenInclude(m => m.ScheduleAvailability)
                .Include(c => c.InstructorUser)
                .Where(c => c.TermId == SelectedTerm.TermId && c.Course.SchoolId == SchoolId).ToListAsync();

            // Courses
            Courses = classes.Select(c => c.Course)
                .Concat(_context.Courses.Where(c => c.SchoolId == SchoolId))
                .Distinct()
                .OrderBy(c => c.Name)
                .Select(c => new CourseViewModel
                {
                    Course = c,
                    Class = c.Classes?.Select(l => new CourseClassViewModel
                    {
                        ClassId = l.ClassId,
                        CourseId = c.CourseId,
                        Capacity = l.Capacity,
                        EnrolledStudentCount = l.Meetings.SelectMany(m => m.StudentMeetings.Select(s => s.StudentId)).Distinct().Count(),
                        Schedule = ScheduleAvailability.GetScheduleString(l.Meetings.Select(m => m.ScheduleAvailability).OrderBy(s => s.DayOfWeek)),
                        InstructorName = l.InstructorUser != null ? l.InstructorUser.LastName + ", " + l.InstructorUser.FirstName : "",
                        MeetingsByNumber = l.Meetings.GroupBy(m => m.MeetingOrderNum).OrderBy(m => m.Key),
                        PassingGradeThreshold = l.PassingGradeThreshold
                    }).FirstOrDefault()
                });

            if (SelectedCourseId == null && Courses.Any())
            {
                SelectedCourseId = Courses.First().Course.CourseId;
            }
        }

        public async Task<IActionResult> OnGetClassFormAsync(int classId, int courseId, int termId)
        {
            Class @class;

            if (classId > 0)
            {
                @class = await _context.Classes
                    .Include(c => c.Course)
                    .Include(c => c.Meetings).ThenInclude(c => c.ScheduleAvailability)
                    .Include(c => c.Term)
                    .FirstOrDefaultAsync(c => c.ClassId == classId);

                if (@class == null)
                {
                    return NotFound();
                }
            }
            else
            {
                @class = new Class() { CourseId = courseId, TermId = termId };
                @class.Term = await _context.Terms.FirstAsync(c => c.TermId == termId);
                @class.Course = await _context.Courses.FirstAsync(c => c.CourseId == courseId);
            }

            // Instructors
            ViewData["instructors"] = await _context.UserRoles.Include(u => u.User)
                .Where(u => u.RoleId == (int)RoleEnum.Instructor)
                .OrderBy(u => u.User.LastName).ThenBy(u => u.User.FirstName)
                .Select(u => new SelectListItem()
                {
                    Value = u.UserId.ToString(),
                    Text = u.User.LastName + ", " + u.User.FirstName
                }).ToListAsync();

            // School Schedule Availabilities
            ViewData["scheduleAvailabilities"] = await _context.ScheduleAvailabilities
                .Where(s => s.SchoolId == @class.Course.SchoolId)
                .OrderBy(s => s.DayOfWeek)
                .ThenBy(s => s.StartTime)
                .ToListAsync();

            return new PartialViewResult()
            {
                ViewName = "_ClassForm",
                ViewData = new ViewDataDictionary<Class>(ViewData, @class)
            };
        }

        public async Task<IActionResult> OnGetStudentsListAsync(int classId)
        {
            var @class = await _context.Classes
                .Include(c => c.Meetings).ThenInclude(m => m.StudentMeetings).ThenInclude(c => c.Student)
                .FirstOrDefaultAsync(c => c.ClassId == classId);

            if (@class == null)
            {
                return NotFound();
            }

            ViewData["classCapacity"] = @class.Capacity;
            var students = @class.Meetings
                .SelectMany(c => c.StudentMeetings.Select(m => m.Student))
                .Distinct()
                .OrderBy(s => s.LastName)
                .ThenBy(s => s.FirstName);

            return new PartialViewResult()
            {
                ViewName = "_StudentsList",
                ViewData = new ViewDataDictionary<IEnumerable<Student>>(ViewData, students)
            };
        }

        public async Task<IActionResult> OnPostSaveClassAsync(Class model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // CONDITION: This class already exists
            if (await _context.Classes.AnyAsync(c => c.ClassId == model.ClassId))
            {
                _context.Classes.Update(model);
            }
            else
            {
                _context.Classes.Add(model);
            }

            await _context.SaveChangesAsync();
            return RedirectToPage(new { termId = model.TermId, courseId = model.CourseId, schoolId = (await _context.Courses.FirstAsync(c => c.CourseId == model.CourseId)).SchoolId });
        }
    }

    public class CourseClassViewModel
    {
        public int ClassId { get; set; }
        public int CourseId { get; set; }
        public string Schedule { get; set; }
        public string InstructorName { get; set; }
        public int EnrolledStudentCount { get; set; }
        public int Capacity { get; set; }
        public double PassingGradeThreshold { get; set; }
        public IEnumerable<IGrouping<int, Meeting>> MeetingsByNumber { get; set; }
    }


    public class CourseViewModel
    {
        public Course Course { get; set; }
        public CourseClassViewModel Class { get; set; }
    }
}