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
    public class RatingModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RatingModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public ApplicantRating ApplicantRating { get; set; }
        public IList<RatingCirterium> RatingCirterium { get; set; }
        public Student Student { get; set; }
        
        [BindProperty]
        public List<int> InputValues { get; set; }

        [BindProperty]
        public string Comment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Student = await _context.Students.FirstOrDefaultAsync(a => a.StudentId == id);

            if (Student == null) return NotFound();

            RatingCirterium = await _context.RatingCirteria.ToListAsync();    //grabbed here to be used in .cshtml

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();
            
            RatingCirterium = await _context.RatingCirteria.ToListAsync();

            for (var i = 0; i < RatingCirterium.Count; i++)
            {
                var applicantRating = new ApplicantRating
                {
                    StudentId = (int) id,
                    UserId = 1,                                                     //THIS IS A TEMP VALUE
                    RatingCiteriumId = RatingCirterium[i].RatingCirteriumId,        
                    TermId = 1,                                                     //THIS IS A TEMP VALUE
                    ScoreAssigned = InputValues[i],                                 
                    DateTime = DateTime.Now,
                    Comments = Comment                                        
                };

                _context.ApplicantRatings.Add(applicantRating);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}