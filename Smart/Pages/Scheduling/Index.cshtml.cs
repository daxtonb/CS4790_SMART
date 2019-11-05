using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Smart.Data;
using Smart.Data.Models;

namespace MyApp.Namespace
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public List<string> DaysOfTheWeek {get; set;} = new List<string>(){"Monday", "Tuesday", "Wednesday", "Thursday", "Friday"};
        public Student MyStudent {get; set;} 

        public IndexModel(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task OnGetAsync(int? studentId)
        {
            if(studentId == null)
            {
                //RedirectToPage("Students/Index");
            }

            MyStudent = _db.Students.FirstOrDefault(i => i.StudentId == studentId);

        }

        public async Task OnPostAsync(int? ClassId)
        {
            //StudentClass class = _db.StudentClasses.FirstOrDefault(c => c.ClassId == ClassId);
            //Find the class
            //Then remove the class
            //Then return back to the index page            
        }
    }
}