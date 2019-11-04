using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Smart.Data;
using Smart.Data.Models;

namespace Smart.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly Smart.Data.ApplicationDbContext _context;

        public CreateModel(Smart.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["StudentStatusId"] = new SelectList(_context.StudentStatuses, "StudentStatusId", "Description");
            return Page();
        }

        [BindProperty]
        public Student Student { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Students.Add(Student);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}