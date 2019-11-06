using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Smart.Data;
using Smart.Data.Models;

namespace Smart.Pages.Attendance
{
    public class IndexModel : PageModel
    {
        private readonly Smart.Data.ApplicationDbContext _context;

        public IndexModel(Smart.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Data.Models.Attendance> Attendance { get;set; }

        public async Task OnGetAsync()
        {
            Attendance = await _context.Attendances
                .Include(a => a.AttendanceStatus)
                .Include(a => a.Class)
                .Include(a => a.Student).ToListAsync();
        }
    }
}
