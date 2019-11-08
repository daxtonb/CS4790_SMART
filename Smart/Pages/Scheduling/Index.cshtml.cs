using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Smart.Data;
using Smart.Data.Models;
using System.Data;

namespace Smart.Pages.Scheduling
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public List<string> DaysOfTheWeek {get; set;} = new List<string>(){"Monday", "Tuesday", "Wednesday", "Thursday", "Friday"};
        public Student MyStudent {get; set;}
        public List<ClassesViewModel> MyClasses { get; set; }

        public IndexModel(ApplicationDbContext dbContext)
        {
            _db = dbContext;
            MyClasses = new List<ClassesViewModel>();
        }

        public void OnGet(int? studentId)
        {
            if(studentId == null)
            {
                //RedirectToPage("Students/Index");
                //hard coded to point to test student austin wilcox
                MyStudent = _db.Students.FirstOrDefault(i => i.StudentId == 3);

                using (SqlConnection conn = new SqlConnection("Server=titan.cs.weber.edu,10433;Database=AustinsFinalProjectTestDatabase;user id=4790;password=4790$tudent"))
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM StudentClass sc INNER JOIN Class c ON c.ClassId = sc.ClassId INNER JOIN Course co ON co.CourseId = c.CourseId INNER JOIN ClassSchedule cs ON cs.ClassId = sc.ClassId AND cs.ClassId = c.ClassId INNER JOIN ScheduleAvailability sa ON sa.ScheduleAvailabilityId = cs.ScheduleAvailabilityId WHERE sc.StudentId = @StudentId", conn))
                    {

                        cmd.Parameters.Add("@StudentId", SqlDbType.Int).Value = 3;

                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            while(rd.Read())
                            {
                                MyClasses.Add(new ClassesViewModel()
                                {
                                    ClassId = Convert.ToInt32(rd["ClassId"].ToString()),
                                    DayOfWeek = Convert.ToInt32(rd["DayOfWeek"].ToString()),
                                    CourseName = rd["Name"].ToString(),
                                    StartTime = rd["StartTime"].ToString(),
                                    EndTime = rd["EndTime"].ToString()
                                });
                            }
                            rd.Close();
                        }
                    }

                    conn.Close();
                }
            }
            else
            {
                MyStudent = _db.Students.FirstOrDefault(i => i.StudentId == studentId);
            }
        }

        public async Task OnPostAsync(int? ClassId)
        {
            //StudentClass class = _db.StudentClasses.FirstOrDefault(c => c.ClassId == ClassId);
            //Find the class
            //Then remove the class
            //Then return back to the index page
            if(ClassId != null)
            {
                Console.WriteLine(ClassId.ToString());
            }
        }

        public class ClassesViewModel
        {
            public int ClassId { get; set; }
            public int DayOfWeek { get; set; }
            public string CourseName { get; set; }
            public string StartTime { get; set; }
            public string EndTime { get; set; }

        }
    }
}