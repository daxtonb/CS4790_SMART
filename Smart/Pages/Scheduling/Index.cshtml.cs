using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Smart.Data;
using Smart.Data.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Smart.Pages.Scheduling
{
    [Authorize(Roles = "Instructor,Admin")]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Student MyStudent { get; set; }
        public Class Classes { get; set; }
        public IEnumerable<Term> Terms { get; set; }

        public IndexModel(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task OnGetAsync(int? studentId)
        {
            if(studentId == null)
            {
                Redirect("../Index");
            }
            else
            {
                MyStudent = await _db.Students.FindAsync(studentId);
                Terms = await _db.Terms.ToListAsync();
            }


        }
    }
}