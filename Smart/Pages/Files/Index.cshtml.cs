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
        public IList<Student> Student { get; set; }

        public int studentIdentifies;

        public async Task OnGetAsync(int studentId)
        {

            studentIdentifies = studentId;

            File =  _context.Files
                .Where(f => f.StudentId == studentId).ToList();

            Student = _context.Students.Where(i => i.StudentId == studentId).ToList();

        }
    }
}
