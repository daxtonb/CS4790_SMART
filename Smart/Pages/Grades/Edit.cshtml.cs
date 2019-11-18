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
    public class EditModel : PageModel
    {
        private readonly Smart.Data.ApplicationDbContext _context;
        public int studentIdentification;

        public EditModel(Smart.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public StudentAssessment StudentAssessment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? idassesment, int studentId)
        {
            studentIdentification = studentId;
            if (idassesment == null)
            {
                return NotFound();
            }

            StudentAssessment = await _context.StudentAssessments
                .Include(s => s.Assessment)
                .Include(s => s.File)
                .Include(s => s.Student).FirstOrDefaultAsync(m => m.AssessmentId == idassesment && m.StudentId == studentId);

            if (StudentAssessment == null)
            {
                return NotFound();
            }
           ViewData["AssessmentId"] = new SelectList(_context.Assessments, "AssessmentId", "Title");
           ViewData["FileId"] = new SelectList(_context.Files, "FileId", "FileName");
           ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "FirstName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, int studentId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(StudentAssessment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentAssessmentExists(StudentAssessment.AssessmentId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new { studentId });
        }

        private bool StudentAssessmentExists(int id)
        {
            return _context.StudentAssessments.Any(e => e.AssessmentId == id);
        }
    }
}
