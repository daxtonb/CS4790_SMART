using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public IList<File> File { get;set; }

        public async Task OnGetAsync()
        {
            File = await _context.File
                .Include(f => f.FileType)
                .Include(f => f.Student).ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }


            var file = new File
            {
                StudentId = (int)9,
                Path = "hysdd"
            };

 
            _context.File.Add(file);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
