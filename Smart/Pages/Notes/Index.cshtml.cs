using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Smart.Data;
using Smart.Data.Models;

namespace Smart.Pages.Notes
{
    public class IndexModel : PageModel
    {
        private readonly Smart.Data.ApplicationDbContext _context;
        public int studentIdentifies;

        public IndexModel(Smart.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Note> Note { get;set; }

    
        public IList<Student> Student { get; set; }

        public async Task OnGet(int studentId)
        {
            Note = _context.Notes
                .Include(n=>n.NoteType)
                .Where(n => n.Studentid == studentId)
                .OrderBy(n => n.CreateDate).ToList();
            
            
            studentIdentifies = studentId;

            Student = _context.Students.Where(i => i.StudentId == studentId).ToList();
           
        }
    }
}
