using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Smart.Data;
using Smart.Data.Models;

namespace Smart.Pages.Notes
{
    public class CreateModel : PageModel
    {
        private readonly Smart.Data.ApplicationDbContext _context;
        public int studentIdentification;
        private readonly UserManager<User> _userManager;

        public CreateModel(Smart.Data.ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int studentId)
        {
            studentIdentification = studentId;
            var user = await _userManager.GetUserAsync(User);
            var idu = user.Id;


            ViewData["NoteTypeId"] = new SelectList(_context.NoteTypes.ToList(), "NoteTypeId", "Description");
        ViewData["Studentid"] = new SelectList(_context.Students.Where(n => n.StudentId == studentId).ToList(), "StudentId", "FirstName");
        ViewData["UserId"] = new SelectList(_context.Users.Where(n => n.Id == user.Id).ToList(), "Id", "Email");
            return Page();
        }

        [BindProperty]
        public Note Note { get; set; }

        public async Task<IActionResult> OnPostAsync(int studentId)
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Notes.Add(Note);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { studentId });
        }
    }
}