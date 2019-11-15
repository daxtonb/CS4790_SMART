using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Smart.Data;
using Smart.Data.Models;

namespace Smart.Pages.Scheduling
{
    public class PublicScheduleModel : PageModel
    {
        public List<StudentPublicSchoolClass> PublicSchedule { get; set; }
        private readonly ApplicationDbContext _db;


        [BindProperty]
        public int StudentID { get; set; }

        [BindProperty]
        [DisplayName("Course Name")]
        public string CourseName { get; set; }
        [BindProperty]
        [DisplayName("Day of The Week")]
        public string DayOfWeek { get; set; }
        [BindProperty]
        [DisplayName("Start Time")]
        public DateTime StartTime { get; set; }
        [BindProperty]
        [DisplayName("End Time")]
        public DateTime EndTime { get; set; }


        public PublicScheduleModel(ApplicationDbContext context)
        {
            _db = context;
        }

        public async Task OnGetAsync(int? studentId)
        {
            if(studentId != null)
            {
                this.StudentID = (int)studentId;
                PublicSchedule = await _db.StudentPublicSchoolClasss.Where(s => s.StudentId == studentId).ToListAsync();
            }
            else
            {
                NotFound();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //Get the current term ID'
            //TODO

            //The values as they are saved in the database
            var monday = Request.Form["Monday"]; //1
            var tuesday = Request.Form["Tuesday"]; //2
            var wednesday = Request.Form["Wednesday"]; //3
            var thursday = Request.Form["Thursday"]; //4
            var friday = Request.Form["Friday"]; //5

            List<ScheduleAvailability> days = new List<ScheduleAvailability>();
            if(monday == 0)
            {
                days.Add(new ScheduleAvailability()
                {
                    DayOfWeek = System.DayOfWeek.Monday,
                    StartTime = StartTime.TimeOfDay,
                    EndTime = EndTime.TimeOfDay
                });
            }
            if (tuesday == 0)
            {
                days.Add(new ScheduleAvailability()
                {
                    DayOfWeek = System.DayOfWeek.Tuesday,
                    StartTime = StartTime.TimeOfDay,
                    EndTime = EndTime.TimeOfDay
                });
            }
            if (wednesday == 0)
            {
                days.Add(new ScheduleAvailability()
                {
                    DayOfWeek = System.DayOfWeek.Wednesday,
                    StartTime = StartTime.TimeOfDay,
                    EndTime = EndTime.TimeOfDay
                });
            }
            if (thursday == 0)
            {
                days.Add(new ScheduleAvailability()
                {
                    DayOfWeek = System.DayOfWeek.Thursday,
                    StartTime = StartTime.TimeOfDay,
                    EndTime = EndTime.TimeOfDay
                });
            }
            if (friday == 0)
            {
                days.Add(new ScheduleAvailability()
                {
                    DayOfWeek = System.DayOfWeek.Friday,
                    StartTime = StartTime.TimeOfDay,
                    EndTime = EndTime.TimeOfDay
                });
            }

            StudentPublicSchoolClass myClass = new StudentPublicSchoolClass()
            {
                CourseName = this.CourseName,
                StudentId = this.StudentID,
                PublicSchoolClassSchedules = days.Select(i => new PublicSchoolClassSchedule()
                {
                    ScheduleAvailabilityd = i
                }).ToList()
            };

            _db.StudentPublicSchoolClasss.Add(myClass);
            await _db.SaveChangesAsync();
            return RedirectToPage("PublicSchedule", new { this.StudentID });
        }


    }
}