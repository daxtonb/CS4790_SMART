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
    public class IndexModel : PageModel
    {
        private readonly Smart.Data.ApplicationDbContext _context;

        public IndexModel(Smart.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<File> File { get;set; }

        public int studentIdentifies;

        public async Task OnGet(int id)
        {
            /*
            File = await _context.Files
                .Include(f => f.FileType)
                .Include(f => f.Student).ToListAsync();*/

            studentIdentifies = id;

            File =  _context.Files
                .Where(f => f.StudentId == id).ToList();

          //  Notes = _context.Notes.Where(n => n.Studentid == studentId).ToList();

        }
    }
}
