using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Smart.Data;
using Smart.Data.Models;

namespace Smart.Pages.Application
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public CreateModel(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
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
            if (!ModelState.IsValid) return Page();

            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            _context.Students.Add(Student);
            await _context.SaveChangesAsync();  //we have to save here so that we can access the db's student Id below

            var studentFromDb = await _context.Students.FindAsync(Student.StudentId);

            if (files.Count > 0)
            {
                var uploads = Path.Combine(webRootPath, "images");
                var extension = Path.GetExtension(files[0].FileName);
                using (var fileStream = new FileStream(Path.Combine(uploads, Student.StudentId.ToString() + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                studentFromDb.Photo = @"\images\" + Student.StudentId + extension;
            }

            await _context.SaveChangesAsync();  //save here to update file path

            return RedirectToPage("./Index");
        }
    }
}