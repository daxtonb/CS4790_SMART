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
            var attendance = await _context.Attendances.Where(a => a.ClassId == classId && a.Date == Convert.ToDateTime(dateTime).Date).ToArrayAsync();


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
                .Include(c => c.ClassSchedules).ThenInclude(c => c.ScheduleAvailability)
                .FirstOrDefaultAsync(c => c.ClassId == classId);

            Attendances = @class.Attendances.GroupBy(a => a.Date.Date, a => a).Select(a => new AttendanceVieModel
            {
                Date = a.Key,
                OnTimeCount = a.Count(x => x.AttendanceStatusId == AttendanceStatusEnum.OnTime),
                LateCount = a.Count(x => x.AttendanceStatusId == AttendanceStatusEnum.Late),
                AbsentCount = a.Count(x => x.AttendanceStatusId == AttendanceStatusEnum.Absent)
            }).OrderByDescending(a => a.Date);

            ViewData["ClassTitle"] = $"{@class.Course.Name} - {@class.Term.TimeOfYear} {@class.Term.StartDate.Year}";
            ViewData["ClassSubtitle"] = ClassSchedule.GetScheduleString(@class.ClassSchedules.OrderBy(c => c.ScheduleAvailability.DayOfWeek));
            ViewData["ClassId"] = @class.ClassId;
        }




        // OnPost uplodingCsvfile
        public async Task<RedirectToPageResult> OnPostUploadCsvAsync()
        {
            var file = Request.Form.Files[0];
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                reader.ReadLine();   
                while (reader.Peek() >= 0)
                {
                    string test = reader.ReadLine();
                    string[] row = test.Split(','); 
                    int classId = int.Parse(row[1]); 
                    DateTime dateTime = Convert.ToDateTime(row[2]); 

                    var attendance = new Data.Models.Attendance()
                    {
                        StudentId = int.Parse(row[0]),
                        ClassId = classId,
                        Date = dateTime.Date,    
                        TimeIn = dateTime.TimeOfDay,
                        AttendanceStatusId = await CalculateAttendance(dateTime, classId)

                    };
                    _context.Attendances.Add(attendance);
                }
                await _context.SaveChangesAsync();
            }


            return RedirectToPage(new { ClassId });
        }

        //Method Calculates Student Attendance
        public async Task<AttendanceStatusEnum> CalculateAttendance(DateTime dt, int classId)
        {

            var scheduleAvailability = (await _context.Classeschedules
                .Include(c => c.ScheduleAvailability)
                .FirstOrDefaultAsync(c => c.ClassId == classId && c.ScheduleAvailability.DayOfWeek == dt.DayOfWeek))?.ScheduleAvailability;

            if (scheduleAvailability == null)
            {
                throw new Exception($"Student does not have class at this time: {dt}");
            }

            if (dt.TimeOfDay > scheduleAvailability.StartTime)
            {
                return AttendanceStatusEnum.Late;
            }
            else
            {
                return AttendanceStatusEnum.OnTime;
            }
    
        }

        public class AttendanceVieModel
        {
            public DateTime Date { get; set; }
            public int OnTimeCount { get; set; }
            public int LateCount { get; set; }
            public int AbsentCount { get; set; }
        }
    }
}