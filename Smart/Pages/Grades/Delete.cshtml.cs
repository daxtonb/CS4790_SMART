﻿using System;
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
        public Assessment Assessment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Assessment = await _context.Assessments
                .Include(a => a.Class).FirstOrDefaultAsync(m => m.AssessmentId == id);

            if (Assessment == null)
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

            Assessment = await _context.Assessments.FindAsync(id);

            if (Assessment != null)
            {
                _context.Assessments.Remove(Assessment);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
