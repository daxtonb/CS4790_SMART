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
        public int? SelectedCourseId { get; set; }
        public async Task OnGet(int? courseId)
        {
            SelectedCourseId = courseId;
            Courses = await _context.Courses
                .Include(c => c.Classes).ThenInclude(c => c.Students)
                .Include(c => c.Classes).ThenInclude(c => c.InstructorUser)
                .Include(c => c.Classes).ThenInclude(c => c.Term)
                .Include(c => c.Classes).ThenInclude(c => c.ClassSchedules).ThenInclude(c => c.ScheduleAvailability)
                .OrderBy(c => c.Name)
                .Select(c => new CourseViewModel()
                {
                    Course = c,
                    ClassCount = c.Classes.Count,
                    Classes = c.Classes
                        .OrderByDescending(l => l.Term.StartDate)
                        .Select(l => new CourseClassViewModel()
                        {
                            ClassId = l.ClassId,
                            CourseId = c.CourseId,
                            Capacity = l.Capacity,
                            EnrolledStudentCount = l.Students.Count,
                            Schedule = CourseClassViewModel.GetScheduleString(l.ClassSchedules.OrderBy(s => s.ScheduleAvailability.DayOfWeek)),
                            InstructorName = l.InstructorUser != null ? l.InstructorUser.LastName + ", " + l.InstructorUser.FirstName : "",
                            TermDescription = l.Term.Description
                        })
                }).ToListAsync();
        }

        public async Task<IActionResult> OnGetCourseForm(int courseId)
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

        public async Task<IActionResult> OnGetClassForm(int classId, int courseId)
        {
            Class @class;

            if (classId > 0)
            {
                @class = await _context.Classes
                    .Include(c => c.ClassSchedules).ThenInclude(c => c.ScheduleAvailability)
                    .FirstOrDefaultAsync(c => c.ClassId == classId);

                if (@class == null)
                {
                    return NotFound();
                }
            }
            else
            {
                @class = new Class() { CourseId = courseId };
            }

            // Terms
            ViewData["terms"] = await _context.Terms
                .OrderBy(t => t.StartDate).ThenBy(t => t.TimeOfYear)
                .Select(t => new SelectListItem()
                {
                    Value = t.TermId.ToString(),
                    Text = t.Description
                }).ToListAsync();

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

        public async Task<IActionResult> OnPostSaveCourse(Course model)
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
            }

            await _context.SaveChangesAsync();
            return RedirectToPage(new { courseId = model.CourseId });
        }

        public async Task<IActionResult> OnPostSaveClass(Class model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var @class = await _context.Classes
                .Include(c => c.ClassSchedules).ThenInclude(c => c.ScheduleAvailability)
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
                @class.TermId = model.TermId;
                @class.Capacity = model.Capacity;
            }

            await _context.SaveChangesAsync();

            // This maybe isn't the best way to handle updating class schedules, but for now it's just easier to delete them all and re-create them
            if (@class.ClassSchedules != null && @class.ClassSchedules.Any())
            {
                _context.Classeschedules.RemoveRange(@class.ClassSchedules);
                _context.ScheduleAvailabilities.RemoveRange(@class.ClassSchedules.Select(c => c.ScheduleAvailability));
                await _context.SaveChangesAsync();
            }

            @class.ClassSchedules = ScheduleAvailabilities.Select(s => new ClassSchedule { ScheduleAvailability = s }).ToList();
            await _context.SaveChangesAsync();
            return RedirectToPage(new { courseId = model.CourseId });
        }
    }

    public class CourseClassViewModel
    {
        public int ClassId { get; set; }
        public int CourseId { get; set; }
        public string TermDescription { get; set; }
        public string Schedule { get; set; }
        public string InstructorName { get; set; }
        public int EnrolledStudentCount { get; set; }
        public int Capacity { get; set; }

        public static string GetScheduleString(IEnumerable<ClassSchedule> classSchedules)
        {
            string dayOfWeek, timeRange, workignString = string.Empty;
            ScheduleAvailability current, next = null;
            var arr = classSchedules.ToArray();

            for (int i = 0; i < arr.Length; i++)
            {
                current = arr[i].ScheduleAvailability;
                next = i < arr.Length - 1 ? arr[i + 1].ScheduleAvailability : null;
                workignString += GetDayOfWeekAbbreviation(current.DayOfWeek);

                // CONDITION: This is the last element or the next element has a different time schedule
                if (i == arr.Length - 1 || (next != null && (next.StartTime != current.StartTime || next.EndTime != current.EndTime)))
                {
                    workignString += " " + current.StartTime.ToString12HourTime() + "-" + current.EndTime.ToString12HourTime() + ", ";
                }
            }

            workignString = workignString.Trim().TrimEnd(',');

            return workignString;
        }

        private static string GetDayOfWeekAbbreviation(DayOfWeek dayOfWeek)
        {
            if (dayOfWeek == DayOfWeek.Tuesday || dayOfWeek == DayOfWeek.Thursday)
            {
                return dayOfWeek.ToString().Substring(0, 2);
            }

            return dayOfWeek.ToString().Substring(0, 1);
        }
    }

    public class CourseViewModel
    {
        public Course Course { get; set; }
        public int ClassCount { get; set; }
        public IEnumerable<CourseClassViewModel> Classes { get; set; }
    }
}