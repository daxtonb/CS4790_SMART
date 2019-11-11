using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Smart.Data;
using Smart.Data.Models;

namespace Smart.Pages.Scheduling
{
    public class PublicScheduleModel : PageModel
    {
        public List<StudentPublicSchoolClass> PublicSchedule { get; set; }
        private readonly ApplicationDbContext _db;

        public PublicScheduleModel(ApplicationDbContext context)
        {
            _db = context;
        }

        public async Task OnGetAsync(int? studentId)
        {
            if(studentId == null)
            {
                PublicSchedule = await _db.StudentPublicSchoolClasss.Where(s => s.StudentId == studentId).ToListAsync();
            }
            else
            {
                NotFound();
            }
        }
    }
}