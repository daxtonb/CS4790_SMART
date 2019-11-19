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

namespace Smart.Pages.Files
{
    public class CreateModel : PageModel
    {
        private readonly Smart.Data.ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public CreateModel(Smart.Data.ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task OnGetAsync(int studentId)
        {
        ViewData["FileTypeId"] = new SelectList(_context.FileTypes, "FileTypeId", "Description");
            ViewData["StudentId"] = new SelectList(_context.Students.Where(n => n.StudentId == studentId), "StudentId", "FirstName");

        }

        [BindProperty]
        public Smart.Data.Models.File File { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
         
      //      if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] pic = null;
                    using (var fs = files[0].OpenReadStream())
                    {
                        using (var ms = new MemoryStream())
                        {
                            fs.CopyTo(ms);
                            pic = ms.ToArray();
                        }
                    }
                    File.FileTypeId = FileTypeEnum.Other;
                    File.FileName = files[0].FileName;
                    File.ByteData = pic;
                }
                _context.Files.Add(File);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            return RedirectToPage("./Index");
        }


    }
}