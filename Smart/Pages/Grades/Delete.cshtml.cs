using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Smart.Data;
using Smart.Data.Models;

namespace Smart.Pages.Grades
{
    public class DeleteModel : PageModel
    {
        private readonly Smart.Data.ApplicationDbContext _context;

        public DeleteModel(Smart.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public StudentAssessment StudentAssessment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StudentAssessment = await _context.StudentAssessments
                .Include(s => s.Assessment)
                .Include(s => s.File)
                .Include(s => s.Student).FirstOrDefaultAsync(m => m.AssessmentId == id);

            if (StudentAssessment == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            StudentAssessment = await _context.StudentAssessments.FindAsync(id);

            if (StudentAssessment != null)
            {
                _context.StudentAssessments.Remove(StudentAssessment);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
