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
        public List<PublicScheduleVM> MyPublicSchoolSchedule { get; set; }
        public List<StudentPublicSchoolClass> PublicSchedule { get; set; }


        public IndexModel(ApplicationDbContext dbContext)
        {
            _db = dbContext;
            ScheduleAvailabilities = new List<ScheduleAvailability>();
            MyPublicSchoolSchedule = new List<PublicScheduleVM>();
        }

        public async Task OnGetAsync(int? studentId)
        {
            Classes = await _db.Classes
                               .Include(i => i.ClassSchedules)
                               .Include(i => i.InstructorUser)
                               .Include(i => i.StudentClasses)
                               //.Where(i => !i.StudentClasses.Any(s => s.StudentId == 3))
                               .ToListAsync();

            foreach (Class c in Classes)
            {
                foreach (ClassSchedule cs in c.ClassSchedules)
                {
                    ScheduleAvailabilities.AddRange(_db.ScheduleAvailabilities
                                                       .Where(i => i.ScheduleAvailabilityId == cs.ScheduleAvailabilityId)
                                                       .ToList());
                }
            }

            RequiredCourses = await _db.Courses
                                       .Where(c => c.IsCoreRequirement)
                                       .ToListAsync();
            

            if (studentId == null)
            {
                MyStudent = _db.Students.Include(c => c.StudentClasses)
                                       .FirstOrDefault(i => i.StudentId == 3);
                MyClasses = await _db.StudentClasses.Include(c => c.Class)
                                                    //.ThenInclude(s => s.ClassSchedules)
                                                    .Where(c => c.StudentId == 3)
                                                    .ToListAsync();

                PublicSchedule = await _db.StudentPublicSchoolClasss
                                         .Where(s => s.StudentId == 3)
                                         .Include(s => s.PublicSchoolClassSchedules)
                                         .ToListAsync();

                foreach (var schoolClass in PublicSchedule)
                {
                    MyPublicSchoolSchedule.Add(new PublicScheduleVM((int)3, schoolClass.TermId, schoolClass.StudentPublicSchoolClassId, _db));
                }
            }
            else
            {
                //Classes = await _db.Classes
                //               .Include(i => i.ClassSchedules)
                //               .Include(i => i.InstructorUser)
                //               .Include(i => i.StudentClasses)
                //               .Where(i => !i.StudentClasses.Any(s => s.StudentId == studentId))
                //               .ToListAsync();

                //foreach (Class c in Classes)
                //{
                //    foreach (ClassSchedule cs in c.ClassSchedules)
                //    {
                //        ScheduleAvailabilities.AddRange(_db.ScheduleAvailabilities
                //                                           .Where(i => i.ScheduleAvailabilityId == cs.ScheduleAvailabilityId)
                //                                           .ToList());
                //    }
                //}

                MyStudent = _db.Students.Include(c => c.StudentClasses)
                                        .FirstOrDefault(i => i.StudentId == studentId);
                MyClasses = await _db.StudentClasses.Include(c => c.Class)
                                                    .Where(c => c.StudentId == studentId)
                                                    .ToListAsync();

                PublicSchedule = await _db.StudentPublicSchoolClasss
                                         .Where(s => s.StudentId == studentId)
                                         .Include(s => s.PublicSchoolClassSchedules)
                                         .ToListAsync();

                foreach (var schoolClass in PublicSchedule)
                {
                    MyPublicSchoolSchedule.Add(new PublicScheduleVM((int)studentId, schoolClass.TermId, schoolClass.StudentPublicSchoolClassId, _db));
                }
            }
        }
        public async Task<IActionResult> OnPostAsync(int id, int studentId)
        {
            //Count is greater than 0 meaning that there are elements found within
            if(Request.Form["RemoveClass"].Count > 0)
            {
                var classToRemove = Request.Form["classId"][0];
                var myStudentId = Request.Form["StudentId"][0];

                if (Request.Form["IsPublicSchool"][0].ToString().Equals("1"))
                {
                    //This is public school and I want to erase that link
                    var tempPublicClass = _db.StudentPublicSchoolClasss.Where(i => i.StudentPublicSchoolClassId == int.Parse(classToRemove)).FirstOrDefault();

                    if(tempPublicClass != null)
                    {
                        _db.StudentPublicSchoolClasss.Remove(tempPublicClass);
                        await _db.SaveChangesAsync();
                    }
                }
                else
                {
                    var tempClass = _db.StudentClasses.Where(i => i.ClassId == int.Parse(classToRemove) && int.Parse(myStudentId) == i.StudentId).FirstOrDefault();

                    if (tempClass != null)
                    {
                        _db.StudentClasses.Remove(tempClass);
                        await _db.SaveChangesAsync();
                    }
                }
                return RedirectToPage();
            }
            else
            {
                if (id != null)
                {
                    StudentClass myClass = new StudentClass() { ClassId = id, StudentId = studentId };

                    var classCheck = _db.StudentClasses.Where(i => i.ClassId == id && i.StudentId == studentId);
                    if(classCheck == null)
                    {
                        _db.StudentClasses.Add(myClass);
                        await _db.SaveChangesAsync();

                        return RedirectToPage();
                    }
                    else
                    {
                        return RedirectToPage();
                    }
                    
                }
            }

            //This right now means that there is an error
            return NotFound();
        }


    }
}