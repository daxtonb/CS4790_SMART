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
using Microsoft.EntityFrameworkCore;

namespace Smart.Pages.Scheduling
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Student MyStudent { get; set; }
        public List<StudentClass> MyClasses { get; set; }
        public List<Course> RequiredCourses { get; set; }
        public List<Class> Classes { get; set; }
        public List<ScheduleAvailability> ScheduleAvailabilities { get; set; }


        public IndexModel(ApplicationDbContext dbContext)
        {
            _db = dbContext;
            ScheduleAvailabilities = new List<ScheduleAvailability>();
        }

        public async Task OnGetAsync(int? studentId)
        {
            RequiredCourses = await _db.Courses.Where(c => c.IsCoreRequirement).ToListAsync();
            Classes = await _db.Classes.Include(i => i.ClassSchedules).Include(i => i.InstructorUser).ToListAsync();
            
            foreach(Class c in Classes)
            {
                foreach(ClassSchedule cs in c.ClassSchedules)
                {
                    ScheduleAvailabilities.AddRange(_db.ScheduleAvailabilities.Where(i => i.ScheduleAvailabilityId == cs.ScheduleAvailabilityId).ToList());
                }
            }


            if (studentId == null)
            {
                MyStudent = _db.Students.Include(c => c.StudentClasses).FirstOrDefault(i => i.StudentId == 3);
                MyClasses = await _db.StudentClasses.Include(c => c.Class).Where(c => c.StudentId == 3).ToListAsync();
            }
            else
            {
                MyStudent = _db.Students.Include(c => c.StudentClasses).FirstOrDefault(i => i.StudentId == studentId);
                MyClasses = await _db.StudentClasses.Include(c => c.Class).Where(c => c.StudentId == studentId).ToListAsync();
            }
        }
        public async Task<IActionResult> OnPostAsync(int id, int studentId)
        {
            if (id != null)
            {
                StudentClass myClass = new StudentClass() { ClassId = id, StudentId = studentId };

                _db.StudentClasses.Add(myClass);
                await _db.SaveChangesAsync();

                return RedirectToPage( );
            }

            //This right now means that there is an error
            return NotFound();
        }

        public async Task<IActionResult> OnDeleteAsync(int id, int studentId)
        {
            if(ModelState.IsValid)
            {
                if(id != null && studentId != null)
                {
                    var myClass = _db.StudentClasses.Where(s => s.StudentId == studentId && s.ClassId == id).FirstOrDefault();

                    _db.StudentClasses.Remove(myClass);
                    await _db.SaveChangesAsync();

                    return RedirectToPage("Index", studentId);
                }
            }

            return NotFound();
        }
    }
}