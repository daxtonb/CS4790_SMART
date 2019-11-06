using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Smart.Data;
using Smart.Data.Models;

namespace Smart.Pages.Attendance
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
        ViewData["AttendanceStatusId"] = new SelectList(_context.AttendanceStatuses, "AttendanceStatusId", "Description");
        ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId");
        ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "FirstName");
            return Page();
        }

        [BindProperty]
        public Data.Models.Attendance Attendance { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attendances.Add(Attendance);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}