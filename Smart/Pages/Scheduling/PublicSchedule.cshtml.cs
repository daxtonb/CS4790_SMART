using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
        public List<PublicScheduleVM> MyPublicSchoolSchedule { get; set; }


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
            MyPublicSchoolSchedule = new List<PublicScheduleVM>();
        }

        public async Task OnGetAsync(int? studentId)
        {
            if(studentId != null)
            {
                this.StudentID = (int)studentId;
                PublicSchedule = await _db.StudentPublicSchoolClasss
                                          .Where(s => s.StudentId == studentId)
                                          .Include(s => s.PublicSchoolClassSchedules)
                                          .ToListAsync();

                foreach(var schoolClass in PublicSchedule)
                {
                    MyPublicSchoolSchedule.Add(new PublicScheduleVM((int)studentId, schoolClass.TermId, schoolClass.StudentPublicSchoolClassId, _db));
                }
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

            //The values as they are saved in the database
            var monday = Request.Form["Monday"]; //1
            var tuesday = Request.Form["Tuesday"]; //2
            var wednesday = Request.Form["Wednesday"]; //3
            var thursday = Request.Form["Thursday"]; //4
            var friday = Request.Form["Friday"]; //5

            List<ScheduleAvailability> days = new List<ScheduleAvailability>();
            if (monday.Count > 0)
            {
                days.Add(new ScheduleAvailability()
                {
                    DayOfWeek = System.DayOfWeek.Monday,
                    StartTime = StartTime.TimeOfDay,
                    EndTime = EndTime.TimeOfDay
                });
            }
            if (tuesday.Count > 0)
            {
                days.Add(new ScheduleAvailability()
                {
                    DayOfWeek = System.DayOfWeek.Tuesday,
                    StartTime = StartTime.TimeOfDay,
                    EndTime = EndTime.TimeOfDay
                });
            }
            if (wednesday.Count > 0)
            {
                days.Add(new ScheduleAvailability()
                {
                    DayOfWeek = System.DayOfWeek.Wednesday,
                    StartTime = StartTime.TimeOfDay,
                    EndTime = EndTime.TimeOfDay
                });
            }
            if (thursday.Count > 0)
            {
                days.Add(new ScheduleAvailability()
                {
                    DayOfWeek = System.DayOfWeek.Thursday,
                    StartTime = StartTime.TimeOfDay,
                    EndTime = EndTime.TimeOfDay
                });
            }
            if (friday.Count > 0)
            {
                days.Add(new ScheduleAvailability()
                {
                    DayOfWeek = System.DayOfWeek.Friday,
                    StartTime = StartTime.TimeOfDay,
                    EndTime = EndTime.TimeOfDay
                });
            }

            Term myTerm = _db.Terms.Where(i => i.StartDate < DateTime.Now && i.EndDate > DateTime.Now).FirstOrDefault();

            StudentPublicSchoolClass myClass = new StudentPublicSchoolClass()
            {
                CourseName = this.CourseName,
                StudentId = this.StudentID,
                PublicSchoolClassSchedules = days.Select(i => new PublicSchoolClassSchedule()
                {
                    ScheduleAvailabilityd = i
                }).ToList(),
                TermId = myTerm.TermId
            };

            _db.StudentPublicSchoolClasss.Add(myClass);
            await _db.SaveChangesAsync();

            return RedirectToPage("PublicSchedule", new { this.StudentID });
        }


    }

    public class PublicScheduleVM
    {
        public int StudentID { get; set; }
        public string CourseName { get; set; }
        public int TermId { get; set; }
        public int ClassId { get; set; }
        public string DaysOfTheWeek { get; set; } = "";
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public List<ScheduleAvailability> Schedule { get; set; }
        private readonly ApplicationDbContext _db;

        public PublicScheduleVM(int studentId, int termId, int classId, ApplicationDbContext context)
        {
            this.StudentID = studentId;
            this.TermId = termId;
            this.ClassId = classId;
            _db = context;
            Schedule = new List<ScheduleAvailability>();

            getSchedule();
        }

        private void getSchedule()
        {
            using (SqlConnection conn = new SqlConnection("Server=titan.cs.weber.edu,10433;Database=AustinsFinalProjectTestDatabase;user id=4790;password=4790$tudent"))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT CASE " +
                                                       "WHEN DayOfWeek = 1 THEN 'M' " +
                                                       "WHEN DayOfWeek = 2 THEN 'Tu' " +
                                                       "WHEN DayOfWeek = 3 THEN 'W' " +
                                                       "WHEN DayOfWeek = 4 THEN 'Th' " +
                                                       "WHEN DayOfWeek = 5 THEN 'F' " +
                                                       "END AS [CourseDayOfWeek], StartTime, EndTime, CourseName FROM StudentPublicSchoolClass c INNER JOIN PublicSchoolClassSchedule s ON s.StudentPublicSchoolClassId = c.StudentPublicSchoolClassId INNER JOIN ScheduleAvailability a ON a.ScheduleAvailabilityId = s.ScheduleAvailabilityId INNER JOIN Term t ON t.TermId = c.TermId WHERE StudentId = @StudentID AND c.TermId = @TermId AND c.StudentPublicSchoolClassId = @ClassId", conn))
                {
                    cmd.Parameters.Add("@StudentId", SqlDbType.Int).Value = this.StudentID;
                    cmd.Parameters.Add("@TermId", SqlDbType.Int).Value = this.TermId;
                    cmd.Parameters.Add("@ClassId", SqlDbType.Int).Value = this.ClassId;

                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while(rd.Read())
                        {
                            CourseName = rd["CourseName"].ToString();
                            StartTime = rd["StartTime"].ToString();
                            EndTime = rd["EndTime"].ToString();
                            DaysOfTheWeek += rd["CourseDayOfWeek"].ToString();
                        }
                        rd.Close();
                    }
                }

                conn.Close();
            }
        }
    }
}