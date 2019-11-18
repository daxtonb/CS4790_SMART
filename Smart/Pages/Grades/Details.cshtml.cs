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
    public class DetailsModel : PageModel
    {
        private readonly Smart.Data.ApplicationDbContext _context;

        public DetailsModel(Smart.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public StudentAssessment StudentAssessment { get; set; }

        public async Task<IActionResult> OnGetAsync(int idassesment, int studentId)
        {
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
            return Page();
        }
    }
}
