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

namespace Smart.Pages.Grades
{
    public class CreateModel : PageModel
    {
        private readonly Smart.Data.ApplicationDbContext _context;

        public IList<Assessment> Assessment { get; set; }
        public IList<Class> Classes { get; set; }
        public IList<StudentClass> StudentClasses { get; set; }

        public CreateModel(Smart.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(int studentId)
        {
        ViewData["AssessmentId"] = new SelectList(_context.Assessments, "AssessmentId", "Title");
        ViewData["FileId"] = new SelectList(_context.Files, "FileId", "FileName");
        ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "FirstName");

           /* var assessment = await _context.Assessments
                 .Include(a => a.StudentAssessments).ThenInclude(f => f.File)
                 .FirstOrDefaultAsync(a => a.AssessmentId == assessmentId);

            Assessment = await _context.Assessments
                .Include()*/





          //  return Page();
        }

        [BindProperty]
        public StudentAssessment StudentAssessment { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.StudentAssessments.Add(StudentAssessment);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}