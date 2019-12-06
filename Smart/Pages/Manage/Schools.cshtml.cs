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

namespace Smart.Pages.Manage
{
    public class SchoolsModel : PageModel
    {

        private readonly ApplicationDbContext _context;

        public IEnumerable<SchoolViewModel> Schools { get; set; }
        public int SelectedSchoolId { get; set; }
        public bool ShowCourses { get; set; }
        public DayOfWeek SelectedDayOfWeek { get; set; }

        public SchoolsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(int? schoolId, DayOfWeek? selectedDayOfWeek, bool? showCourses)
        {
            var schools = await _context.Schools.Include(s => s.ScheduleAvailabilities).Include(s => s.Courses).ToListAsync();
            Schools = schools.Select(s => new SchoolViewModel
            {
                School = s,
                ScheduleAvailabilitiesByDay = s.ScheduleAvailabilities
                                                .GroupBy(s => s.DayOfWeek)
                                                .ToDictionary(s => s.Key, s => s.OrderBy(a => a.StartTime).ToList())
            }).ToList();

            SelectedSchoolId = schoolId ?? schools.First().SchoolId;
            SelectedDayOfWeek = selectedDayOfWeek ?? DayOfWeek.Monday;
            ShowCourses = showCourses == null || (showCourses.HasValue && showCourses.Value == true);
        }

        public async Task<IActionResult> OnGetSchoolFormAsync(int? schoolId)
        {
            School school;
            if (schoolId.HasValue)
            {
                school = await _context.Schools.FirstOrDefaultAsync(s => s.SchoolId == schoolId.Value);
                if (school == null)
                {
                    return NotFound();
                }
            }
            else
            {
                school = new School();
            }

            return new PartialViewResult()
            {
                ViewName = "_SchoolForm",
                ViewData = new ViewDataDictionary<School>(ViewData, school)
            };
        }

        public async Task<IActionResult> OnPostSaveSchoolAsync(School model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (model.SchoolId == 0)
            {
                _context.Schools.Add(model);
            }
            else
            {
                var school = await _context.Schools.FirstOrDefaultAsync(s => s.SchoolId == model.SchoolId);
                if (school == null)
                {
                    return NotFound();
                }

                school.Name = model.Name;
            }

            await _context.SaveChangesAsync();

            return RedirectToPage(new { schoolId = model.SchoolId });
        }

        public async Task<IActionResult> OnGetScheduleAvailabilityFormAsync(int schoolId, DayOfWeek dayOfWeek, int? scheduleAvailabilityId)
        {
            ScheduleAvailability scheduleAvailability;
            if (scheduleAvailabilityId.HasValue)
            {
                scheduleAvailability = await _context.ScheduleAvailabilities.FirstOrDefaultAsync(s => s.SchoolId == schoolId && s.ScheduleAvailabilityId == scheduleAvailabilityId.Value);
                if (scheduleAvailability == null)
                {
                    return NotFound();
                }
            }
            else
            {
                scheduleAvailability = new ScheduleAvailability() { DayOfWeek = dayOfWeek, SchoolId = schoolId };
            }

            return new PartialViewResult()
            {
                ViewName = "_ScheduleAvailabilityForm",
                ViewData = new ViewDataDictionary<ScheduleAvailability>(ViewData, scheduleAvailability)
            };
        }

        public async Task<IActionResult> OnPostSaveScheduleAvailability(ScheduleAvailability model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (model.ScheduleAvailabilityId == 0)
            {
                _context.ScheduleAvailabilities.Add(model);
            }
            else
            {
                var scheduleAvailability = await _context.ScheduleAvailabilities.FirstOrDefaultAsync(s => s.ScheduleAvailabilityId == model.ScheduleAvailabilityId);
                if (scheduleAvailability == null)
                {
                    return NotFound();
                }

                scheduleAvailability.StartTime = model.StartTime;
                scheduleAvailability.EndTime = model.EndTime;
            }

            await _context.SaveChangesAsync();

            return RedirectToPage(new { schoolId = model.SchoolId, selectedDayOfWeek = (int)model.DayOfWeek, showCourses = false });
        }



        public async Task<IActionResult> OnGetCourseFormAsync(int schoolId, int courseId)
        {
            Course course;

            if (courseId > 0)
            {
                course = await _context.Courses.FirstOrDefaultAsync(c => c.CourseId == courseId && c.SchoolId == schoolId);
                if (course == null)
                {
                    return NotFound();
                }
            }
            else
            {
                course = new Course() { SchoolId = schoolId };
            }

            return new PartialViewResult()
            {
                ViewName = "_CourseForm",
                ViewData = new ViewDataDictionary<Course>(ViewData, course)
            };
        }

        public async Task<IActionResult> OnPostSaveCourseAsync(Course model)
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
            return RedirectToPage(new { schoolId = model.SchoolId, showCourses = true });
        }

        public class SchoolViewModel
        {
            public School School { get; set; }
            public Dictionary<DayOfWeek, List<ScheduleAvailability>> ScheduleAvailabilitiesByDay { get; set; }
        }
    }
}