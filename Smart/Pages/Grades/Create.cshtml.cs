using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Smart.Data;
using Smart.Data.Models;

namespace Smart.Pages.Grades
{
    public class CreateModel : PageModel
    {
        private readonly Smart.Data.ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public IList<Assessment> Assessment { get; set; }


        public Assessment AssessmentOne { get; set; }

        public IList<Class> Classes { get; set; }
        public IList<StudentClass> StudentClasses { get; set; }
        public int studentIdentification;


        public CreateModel(Smart.Data.ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task OnGetAsync(int studentId)
        {
            studentIdentification = studentId;
            var user = await _userManager.GetUserAsync(User);
            var idu = user.Id;

           
        ViewData["FileId"] = new SelectList(_context.Files, "FileId", "FileName");
        ViewData["StudentId"] = new SelectList(_context.Students.Where(n => n.StudentId == studentId), "StudentId", "FirstName");

            /* var assessment = await _context.Assessments
                  .Include(a => a.StudentAssessments).ThenInclude(f => f.File)
                  .FirstOrDefaultAsync(a => a.AssessmentId == assessmentId);*/
          /*  
            StudentClasses = await _context.StudentClasses
                .Include(n => n.Class)
                .Where(n => n.Class.InstructorUserId == idu && n.StudentId == studentId).ToListAsync();

            foreach (var item in StudentClasses)
            {
                AssessmentOne = await _context.Assessments
                .Include(n => n.Class)
                .Include(n => n.StudentAssessments)
                .FirstAsync(n => n.Class.InstructorUserId == idu && n.Class.ClassId == item.ClassId);
                _context.Assessments.Add(AssessmentOne);
            }*/

            //var num =Assessment.Count;

            ViewData["AssessmentId"] = new SelectList(_context.Assessments, "AssessmentId", "Title");


            //  return Page();
        }

        [BindProperty]
        public StudentAssessment StudentAssessment { get; set; }

        public async Task<IActionResult> OnPostAsync(int studentId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.StudentAssessments.Add(StudentAssessment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { studentId });
        }
    }
}