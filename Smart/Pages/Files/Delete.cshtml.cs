using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Smart.Data;
using Smart.Data.Models;

namespace Smart.Pages.Files
{
    public class DeleteModel : PageModel
    {
        private readonly Smart.Data.ApplicationDbContext _context;
        public int studentIdentification;

        public DeleteModel(Smart.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public File File { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, int studentId)
        {
            studentIdentification = studentId;
            if (id == null)
            {
                return NotFound();
            }

            File = await _context.Files
                .Include(f => f.FileType)
                .Include(f => f.Student).FirstOrDefaultAsync(m => m.FileId == id);

            if (File == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, int studentId)
        {
            if (id == null)
            {
                return NotFound();
            }

            File = await _context.Files.FindAsync(id);
            if (File != null)
            {
                _context.Files.Remove(File);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new { studentId });
        }
    }
}
