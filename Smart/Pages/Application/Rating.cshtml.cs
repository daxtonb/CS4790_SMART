using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Smart.Data;
using Smart.Data.Models;

//Okay, so each applicant has multiple ratings. Each user only rates once per student. 
// 1. ApplicantRating.StudentId obviously matches Student.StudentId
// 2. ApplicantRatingId is a unique PK for each rating
// 3. UserId connects to the AspNetUser.Id (is the identity of the reviewer)
// 4. RatingCriteriumId ???
// 5. TermId ?? (does it change every term somehow)?

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

        public int studentScore;
        public int possibleScore;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Student = await _context.Students.FirstOrDefaultAsync(a => a.StudentId == id);

            if (Student == null) return NotFound();

            RatingCirterium = await _context.RatingCirteria.ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            ApplicantRating newApplicationRating = new ApplicantRating  //do i need to do this?
            {
                ApplicantRatingId = (int)id //convert from int? to int
            };

            RatingCirterium = await _context.RatingCirteria.ToListAsync();

            foreach (var item in RatingCirterium)
            {
                possibleScore += item.MaxScore;
            }

            //newApplicationRating.Score = studentScore / possibleScore ? (given in a percentage?)

            _context.ApplicantRatings.Add(newApplicationRating);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}