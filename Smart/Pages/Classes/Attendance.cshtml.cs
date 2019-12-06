using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Smart.Data;
using Smart.Data.Models;

namespace Smart.Pages.Classes
{
    public class AttendanceModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public int ClassId { set; get; }


        public IEnumerable<AttendanceVieModel> Attendances { get; set; }

        public AttendanceModel(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> OnGetAttendanceList(string dateTime, int classId)
        {
            var attendance = await _context.Attendances.Where(a => a.ClassId == classId && a.Date == Convert.ToDateTime(dateTime)).ToArrayAsync();


            return new PartialViewResult()
            {
                ViewName = "_AttendanceForm",
                ViewData = new ViewDataDictionary<IEnumerable<Data.Models.Attendance>>(ViewData, attendance)


            };
        }
        public async Task OnGetAsync(int classId)
        {
            ClassId = classId;
            var @class = await _context.Classes
                .Include(c => c.Attendances)
                .Include(c => c.Course)
                .Include(c => c.Term)
                .Include(c => c.Meetings).ThenInclude(c => c.ScheduleAvailability)
                .FirstOrDefaultAsync(c => c.ClassId == classId);

            Attendances = @class.Attendances.GroupBy(a => a.Date.Date, a => a).Select(a => new AttendanceVieModel
            {
                Date = a.Key,
                Count = a.Count()
            }).OrderByDescending(a => a.Date);

            ViewData["ClassId"] = @class.ClassId;
        }

        public async Task<RedirectToPageResult> OnPostUploadCsvAsync()
        {
            var file = Request.Form.Files[0];
            var courses = await _context.Courses.ToListAsync();
            Class @class = null;
            string previousCourseName = null;

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                reader.ReadLine();   // Skip column headers
                while (reader.Peek() >= 0)
                {
                    // FORMAT: <StudentId>,<StudentName>,<CourseName>,<Date>,Missed,<TimeIn>
                    string[] columns = reader.ReadLine().Split(',');
                    int studentId = int.Parse(columns[0]);
                    string courseName = columns[2];
                    DateTime date = Convert.ToDateTime(columns[3]).Date;
                    TimeSpan? timeIn = null;
                    
                    // CONDITION: A valid time was provided
                    if (columns[5] != "Missed")
                    {
                        int[] timeParts = columns[5].Split(':').Select(x => int.Parse(x)).ToArray();
                        timeIn = new TimeSpan(timeParts[0], timeParts[1], 0);
                    }

                    // CONDITION: We have not previously looked up this class in the database
                    if (courseName != previousCourseName)
                    {
                        // Find a class that is for the given course name that has an active term for the given date
                        @class = await _context.Classes
                        .Include(c => c.Course)
                        .Include(c => c.Term).
                        FirstOrDefaultAsync(c => c.Course.Name.Equals(courseName, StringComparison.OrdinalIgnoreCase) && c.Term.StartDate >= date && c.Term.EndDate <= date);
                    }

                    // CONDITION: Class and student exist
                    if (@class != null && await _context.Students.AnyAsync(s => s.StudentId == studentId))
                    {
                        var attendance = await _context.Attendances.FirstOrDefaultAsync(a => a.StudentId == studentId && a.ClassId == @class.ClassId && a.Date == date);
                        // CONDITION: This is not a previously recorded attendance
                        if (attendance == null)
                        {
                            _context.Attendances.Add(new Data.Models.Attendance()
                            {
                                StudentId = studentId,
                                ClassId = @class.ClassId,
                                Date = date.Date,
                                TimeIn = timeIn
                            });
                        }
                        else
                        {
                            attendance.TimeIn = timeIn; // Update the time
                            attendance.Comments = null; // Remove previous comments
                        }
                    }

                    previousCourseName = courseName;
                }
                await _context.SaveChangesAsync();
            }


            return RedirectToPage(new { ClassId });
        }

        public class AttendanceVieModel
        {
            public DateTime Date { get; set; }
            public int Count { get; set; }
        }
    }
}