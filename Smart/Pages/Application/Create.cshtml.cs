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

        [BindProperty]
        public Student Student { get; set; }

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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            _context.Students.Add(Student);
            await _context.SaveChangesAsync();  //we have to save here so that we can access the db's student Id below

            #region Photo
            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            foreach(var file in files)
            {
                if (file.Name == "photo")
                {
                    var uploads = Path.Combine(webRootPath, "images");
                    var extension = Path.GetExtension(files[0].FileName);
                    using (var fileStream = new FileStream(Path.Combine(uploads, Student.StudentId.ToString() + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    Student.Photo = @"\images\" + Student.StudentId + extension;
                }
                else //from the document upload box
                {
                    byte[] doc = null;

                    using (var fs = file.OpenReadStream())
                    {
                        using (var ms = new MemoryStream())
                        {
                            fs.CopyTo(ms);
                            doc = ms.ToArray();
                        }
                    }

                    var newFile = new Data.Models.File
                    {
                        ByteData = doc,
                        FileTypeId = FileTypeEnum.Other,
                        FileName = file.FileName,
                        StudentId = Student.StudentId
                    };

                    _context.Files.Add(newFile);
                    await _context.SaveChangesAsync();
                }
            }

            await _context.SaveChangesAsync();  //save here to update file path
            #endregion

            return RedirectToPage("./Index");
        }
    }
}