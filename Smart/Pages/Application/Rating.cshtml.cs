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

        public Student Student { get; set; }

        //I don't know if ratings needs a onGet because it the ratings don't exist yet in the database and what not...
        //public async Task<IActionResult> OnGetAsync(int? id)
        //{
        //    if (id == null) return NotFound();

        //    //ApplicantRating = await _context.ApplicantRatings.FirstOrDefaultAsync(a => ApplicantRating.ApplicantRatingId == id);

        //    //if (ApplicantRating == null) return NotFound();

        //    return Page();
        //}

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null) return NotFound();

            Student = await _context.Students.FirstOrDefaultAsync(a => a.StudentId == id);

            if (Student == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null) return NotFound();

            ApplicantRating newApplicationRating = new ApplicantRating
            {
                ApplicantRatingId = (int)id //convert from int? to int
            };

            _context.ApplicantRatings.Add(newApplicationRating);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}
