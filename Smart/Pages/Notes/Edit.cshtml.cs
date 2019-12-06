using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Smart.Data;
using Smart.Data.Models;

namespace Smart.Pages.Notes
{
    public class EditModel : PageModel
    {
        private readonly Smart.Data.ApplicationDbContext _context;
        public int studentIdentification;
        private readonly UserManager<User> _userManager;

        public EditModel(Smart.Data.ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public Note Note { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id, int studentId)
        {
            studentIdentification = studentId;
            var user = await _userManager.GetUserAsync(User);
            var idu = user.Id;


            if (id == null)
            {
                return NotFound();
            }

            Note = await _context.Notes
                .Include(n => n.NoteType)
                .Include(n => n.Student)
                .Include(n => n.User).FirstOrDefaultAsync(m => m.NoteId == id);

            if (Note == null)
            {
                return NotFound();
            }

        
            ViewData["NoteTypeId"] = new SelectList(_context.NoteTypes.ToList(), "NoteTypeId", "Description");
            ViewData["Studentid"] = new SelectList(_context.Students.Where(n => n.StudentId == studentId).ToList(), "StudentId", "FirstName");
           ViewData["UserId"] = new SelectList(_context.Users.Where(n => n.Id == user.Id).ToList(), "Id", "Email");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id, int studentId)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Note).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NoteExists(Note.NoteId))
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

        private bool NoteExists(int id)
        {
            return _context.Notes.Any(e => e.NoteId == id);
        }
    }
}
