using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Smart.Data;
using Smart.Data.Models;

namespace Smart.Pages.Attendance
{
    public class DeleteModel : PageModel
    {
        private readonly Smart.Data.ApplicationDbContext _context;

        public DeleteModel(Smart.Data.ApplicationDbContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Attendance = await _context.Attendances.FindAsync(id);

            if (Attendance != null)
            {
                _context.Attendances.Remove(Attendance);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
