using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Smart.Data;
using Smart.Data.Models;

namespace Smart.Pages.RatingCriterium
{
    public class DetailsModel : PageModel
    {
        private readonly Smart.Data.ApplicationDbContext _context;

        public DetailsModel(Smart.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public RatingCirterium RatingCirterium { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RatingCirterium = await _context.RatingCirteria.FirstOrDefaultAsync(m => m.RatingCirteriumId == id);

            if (RatingCirterium == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
