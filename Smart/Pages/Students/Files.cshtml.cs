using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Smart.Data;
using Smart.Data.Models;

namespace Smart.Pages.Students
{
    public class FilesModel : PageModel
    {
        private readonly Smart.Data.ApplicationDbContext _context;

        public FilesModel(Smart.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IList<File> File { get;set; }

        [BindProperty]
        public File file { get; set; }

        public async Task OnGetAsync()
        {
            File = await _context.File
                .Include(f => f.FileType)
                .Include(f => f.Student).ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(int? id, IFormFile file1)
        {
            if (!ModelState.IsValid)
            {
               // return Page();
            }

           // file.FileId = 1;
            file.FileTypeId = 1;
            file.StudentId = 9;
            var valuor = file.Path;
 
            _context.File.Add(file);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }


    }
}
