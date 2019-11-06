using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Smart.Data;
using Smart.Data.Models;

namespace Smart.Pages.Students
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public CreateModel(ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
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

            // Work on Saving the Image
            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var studentFromDb = await
                _context.Students.FindAsync(Student.StudentId);

            if (files.Count > 0)
            {
                //file(s) have been submitted for processing
                var uploads = Path.Combine(webRootPath, "images");
                var extension = Path.GetExtension(files[0].FileName);
                using (var fileStream = new FileStream(Path.Combine(uploads, Student.StudentId.ToString() + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                studentFromDb.Photo = @"\images\" + Student.StudentId + extension;
            }
            else
            {
                //  var uploads = Path.Combine(webRootPath + @"\images\" + SD.DefaultFoodImage);
                //    System.IO.File.Copy(uploads, webRootPath + @"\images\" + Student.StudentId + ".png");
            }


            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}