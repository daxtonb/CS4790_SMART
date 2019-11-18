using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Smart.Data;
using Smart.Data.Models;

namespace Smart.Pages.Grades
{
    public class IndexModel : PageModel
    {
        private readonly Smart.Data.ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public IndexModel(Smart.Data.ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<StudentAssessment> StudentAssessment { get;set; }
        public IList<Class> Class { get; set; }

        public async Task OnGetAsync(int studentId)
        {
            var user = await _userManager.GetUserAsync(User);
            var idu = user.Id;

            Class = _context.Classes.Where(n => n.InstructorUserId == idu).ToList();

           // StudentAssessment = _context.StudentAssessments.Where(n => n.StudentId == studentId).ToList();

            StudentAssessment = await _context.StudentAssessments
                .Include(s => s.Assessment)
                .Include(s => s.Assessment.Class)
                .Where(s => s.Assessment.Class.InstructorUserId == idu && s.StudentId == studentId)
                .ToListAsync();
                
             
            /*
             * 
             * public async Task OnGet(int studentId)
                {
            Note = _context.Notes.Where(n => n.Studentid == studentId).ToList();
            studentIdentifies = studentId;

            Student = _context.Students.Where(i => i.StudentId == studentId).ToList();
           
                }
             */
        }
    }
}
