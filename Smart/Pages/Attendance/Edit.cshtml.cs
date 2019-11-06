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

namespace Smart.Pages.Attendance
{
    public class EditModel : PageModel
    {
        private readonly Smart.Data.ApplicationDbContext _context;

        public EditModel(Smart.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Data.Models.Attendance Attendance { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Attendance = await _context.Attendances
                .Include(a => a.AttendanceStatus)
                .Include(a => a.Class)
                .Include(a => a.Student).FirstOrDefaultAsync(m => m.StudentId == id);

            if (Attendance == null)
            {
                return NotFound();
            }
           ViewData["AttendanceStatusId"] = new SelectList(_context.AttendanceStatuses, "AttendanceStatusId", "Description");
           ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassId");
           ViewData["StudentId"] = new SelectList(_context.Students, "StudentId", "FirstName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Attendance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttendanceExists(Attendance.StudentId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AttendanceExists(int id)
        {
            return _context.Attendances.Any(e => e.StudentId == id);
        }
    }
}
