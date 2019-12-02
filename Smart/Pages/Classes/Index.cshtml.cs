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
        [BindProperty]
        public IEnumerable<ScheduleAvailability> ScheduleAvailabilities { get; set; }
        public IEnumerable<Term> Terms { get; set; }
        public IEnumerable<School> Schools { get; set; }
        public int? SelectedCourseId { get; set; }
        public int SelectedTermId { get; set; }
        public int SchoolId { get; set; }

        public async Task OnGetAsync(int? schoolId, int? termId, int? courseId)
        {
            SelectedCourseId = courseId;

            // Schools
            Schools = await _context.Schools.ToListAsync();
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
                SelectedTermId = Terms.FirstOrDefault(t => t.StartDate >= DateTime.Today && DateTime.Today <= t.EndDate)?.TermId ?? Terms.First().TermId;
            }
            else
            {
                SelectedTermId = termId.Value;
            }

            // Classes
            var classes = await _context.Classes
                .Include(c => c.Course)
                .Include(c => c.Meetings).ThenInclude(m => m.StudentMeetings)
                .Include(c => c.Meetings).ThenInclude(m => m.ScheduleAvailability)
                .Where(c => c.TermId == SelectedTermId && c.Course.SchoolId == SchoolId).ToListAsync();

            // Courses
            Courses = classes.Select(c => c.Course).Distinct().OrderBy(c => c.Name).Select(c => new CourseViewModel
            {
                Course = c,
                Classes = c.Classes.Select(l => new CourseClassViewModel
                {
                    ClassId = l.ClassId,
                    CourseId = c.CourseId,
                    Capacity = l.Capacity,
                    EnrolledStudentCount = l.Meetings.SelectMany(m => m.StudentMeetings.Select(s => s.StudentId)).Distinct().Count(),
                    Schedule = ScheduleAvailability.GetScheduleString(l.Meetings.Select(m => m.ScheduleAvailability).OrderBy(s => s.DayOfWeek)),
                    InstructorName = l.InstructorUser != null ? l.InstructorUser.LastName + ", " + l.InstructorUser.FirstName : "",
                })
            });
            
            if (SelectedCourseId == null && Courses.Any())
            {
                SelectedCourseId = Courses.First().Course.CourseId;
            }
        }

        public async Task<IActionResult> OnGetCourseFormAsync(int courseId)
        {
            Course course;

            if (courseId > 0)
            {
                course = await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == courseId);
                if (course == null)
                {
                    return NotFound();
                }
            }
            else
            {
                course = new Course();
            }

            return new PartialViewResult()
            {
                ViewName = "_CourseForm",
                ViewData = new ViewDataDictionary<Course>(ViewData, course)
            };
        }

        public async Task<IActionResult> OnGetClassFormAsync(int classId, int courseId, int termId)
        {
            Class @class;

            if (classId > 0)
            {
                @class = await _context.Classes
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
                @class = new Class() { CourseId = courseId };
                @class.TermId = termId;
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

        public async Task<IActionResult> OnPostSaveCourseAsync(Course model, int termId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            // CONDITION: This is a new course. Add to database.
            if (model.CourseId == 0)
            {
                _context.Courses.Add(model);
            }
            else
            {
                var course = await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == model.CourseId);
                if (course == null)
                {
                    return NotFound();
                }

                course.Name = model.Name;
                course.IsCoreRequirement = model.IsCoreRequirement;
            }

            await _context.SaveChangesAsync();
            return RedirectToPage(new { termId, courseId = model.CourseId });
        }

        public async Task<IActionResult> OnPostSaveClassAsync(Class model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var @class = await _context.Classes
                .Include(c => c.Meetings).ThenInclude(c => c.ScheduleAvailability)
                .Include(c => c.Term)
                .FirstOrDefaultAsync(c => c.ClassId == model.ClassId && c.CourseId == c.CourseId);

            // CONDITION: This is a new class
            if (@class == null)
            {
                @class = model;
                _context.Classes.Add(@class);
            }
            else
            {
                // Update the existing class
                @class.InstructorUserId = model.InstructorUserId;
                @class.Capacity = model.Capacity;
            }

            await _context.SaveChangesAsync();

            // This maybe isn't the best way to handle updating class schedules, but for now it's just easier to delete them all and re-create them
            if (@class.Meetings != null && @class.Meetings.Any())
            {
                _context.Meetings.RemoveRange(@class.Meetings);
                _context.ScheduleAvailabilities.RemoveRange(@class.Meetings.Select(c => c.ScheduleAvailability));
                await _context.SaveChangesAsync();
            }

            @class.Meetings = ScheduleAvailabilities.Select(s => new Meeting { ScheduleAvailability = s }).ToList();
            await _context.SaveChangesAsync();
            return RedirectToPage(new { @class.TermId, courseId = @class.CourseId });
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
    }

    public class CourseViewModel
    {
        public Course Course { get; set; }
        public IEnumerable<CourseClassViewModel> Classes { get; set; }
    }
}