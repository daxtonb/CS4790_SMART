using LINQtoCSV;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Smart.Data;
using Smart.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Smart.Pages.Classes
{
    public class AttendanceModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IEnumerable<AttendanceVieModel> Attendances { get; set; }

        [BindProperty]
        public Attendance Attendance { get; set; }

        public AttendanceModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(int classId)
        {
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
        public async Task<PageResult> OnPostUploadCsvAsync()
        {
            var file = Request.Form.Files[0];
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                reader.ReadLine();
                while (reader.Peek() >= 0)
                {
                    string test = reader.ReadLine();
                    string[] row = test.Split(',');
                    string time = row[2] + " " + row[3];
                    DateTime dateTime = Convert.ToDateTime(time);
                    // DateTime dateTime = Convert.ToDateTime(row[3]);

                    var attendance = new Data.Models.Attendance()
                    {
                        StudentId = int.Parse(row[0]),
                        ClassId = int.Parse(row[1]),
                        Date = dateTime.Date,
                        TimeIn = dateTime.TimeOfDay,
                        AttendanceStatusId = AttendanceStatusEnum.Late


                        //StudentId = int.Parse(row[0]),
                        //ClassId = int.Parse(row[1]),
                        //Date = dateTime.Date,
                        //TimeIn = dateTime.TimeOfDay,
                        //AttendanceStatusId = CalculateAttendance(dt)
                    };
                    _context.Attendances.Add(attendance);
                }
                await _context.SaveChangesAsync();
            }


            return Page();
        }


       /* public async int CalculateAttendance(DateTime dt)
        {


            var scheduleAvailability = _context.Classeschedules
                .Include(c => c.ScheduleAvailability)
                .FirstOrDefaultAsync(c => c.ClassId == ClassId && c.ScheduleAvailability
                .Any(s => s.DayOfWeek == dateTime.DayOfWeek))
                .ScheduleAvailability;
            
            AttendanceStatusEnum attendanceStatusId;
            
            if (dt.TimeOfDay > scheduleAvailability.StartTime)
            {
                attendanceStatusId = AttendanceStatusEnum.Late;
            }
            else if (dt.TimeOfDay == scheduleAvailability.startTime)
            {
                attendanceStatusId = AttendanceStatusEnum.OnTime;
            }
            else
            {
                attendanceStatusId = AttendanceStatusEnum.Absent;
            }

            return 0;
        }
        */

        public class AttendanceVieModel
        {
            public DateTime Date { get; set; }
            public int OnTimeCount { get; set; }
            public int LateCount { get; set; }
            public int AbsentCount { get; set; }
        }
    }
}