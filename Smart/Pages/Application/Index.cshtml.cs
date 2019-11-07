using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Smart.Data;
using Smart.Data.Models;

namespace Smart.Pages.Application
{
    public class IndexModel : PageModel
    {
        private readonly Smart.Data.ApplicationDbContext _context;

        public IndexModel(Smart.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Student> Student { get; set; }
        public List<ApplicantRating> ApplicantRating { get; set; }
        public int ScorePossible { get; set; }

        public async Task OnGetAsync()
        {
            Student = await _context.Students.Where(a => a.StudentStatusId == StudentStatusEnum.Applicant || a.StudentStatusId == StudentStatusEnum.Waitlisted).ToListAsync(); //.ToListAsync();  //change to where status != active or graduated
            ApplicantRating = await _context.ApplicantRatings.ToListAsync();
            var rc= await _context.RatingCirteria.ToListAsync();
            
            foreach(var criteria in rc)
            {
                ScorePossible += criteria.MaxScore;
            }
        }
    }
}