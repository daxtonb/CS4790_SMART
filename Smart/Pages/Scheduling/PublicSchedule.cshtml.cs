using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Smart.Pages.Scheduling
{
    public class PublicScheduleModel : PageModel
    {
        public int StudentID { get; set; } = 3;
        public string CourseName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string DayOfWeek { get; set; }
        public void OnGet()
        {

        }
    }
}