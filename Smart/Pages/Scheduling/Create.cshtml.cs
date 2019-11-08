using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Smart.Data;
using Smart.Data.Models;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions;


namespace Smart.Pages.Scheduling
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public List<ScheduleAvailability> Schedule { get; set; }
        public string Day { get; set; }
        [BindProperty]
        public int ClassId { get; set; }
        //HARD CODED FOR Austin Wilcox
        private int StudentId { get; set; } = 3;
        public CreateModel(ApplicationDbContext dbcontext)
        {
            _db = dbcontext;
        }

        public async Task OnGetAsync(int id)
        {
            switch(id)
            {
                case 1: Day = "Monday"; break;
                case 2: Day = "Tuesday"; break;
                case 3: Day = "Wednesday"; break;
                case 4: Day = "Thursday"; break;
                case 5: Day = "Friday"; break;
            }

            //TODO
            //Need help converting this into ef Core
            List<SelectListItem> schedule = new List<SelectListItem>();
            using (SqlConnection conn = new SqlConnection("Server=titan.cs.weber.edu,10433;Database=AustinsFinalProjectTestDatabase;user id=4790;password=4790$tudent"))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Class c " +
                                                       "INNER JOIN ClassSchedule cs ON cs.ClassId = c.ClassId " +
                                                       "INNER JOIN ScheduleAvailability sa ON sa.ScheduleAvailabilityId = cs.ScheduleAvailabilityId " +
                                                       "INNER JOIN Course co ON co.CourseId = c.CourseId " +
                                                       "WHERE DayOfWeek = @DayOfWeek", conn))
                {
                    cmd.Parameters.Add("@DayOfWeek", SqlDbType.Int).Value = id;

                    using (SqlDataReader rd = cmd.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            schedule.Add(new SelectListItem() { Value = rd["ClassId"].ToString(), Text = $"{rd["Name"].ToString()} Start: {rd["StartTime"].ToString()}, End: {rd["EndTime"].ToString()}" });
                        }
                        rd.Close();
                    }
                }

                conn.Close();
            }

            ViewData["schedule"] = schedule;
        }

        public IActionResult OnPost()
        {
            using (SqlConnection conn = new SqlConnection("Server=titan.cs.weber.edu,10433;Database=AustinsFinalProjectTestDatabase;user id=4790;password=4790$tudent"))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("INSERT INTO StudentClass (StudentId, ClassId) VALUES (@StudentId, @ClassId)", conn))
                {
                    cmd.Parameters.Add("@StudentId", SqlDbType.Int).Value = this.StudentId;
                    cmd.Parameters.Add("@ClassId", SqlDbType.Int).Value = this.ClassId;

                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }

            return RedirectToPage("./Index");
        }


    }
}