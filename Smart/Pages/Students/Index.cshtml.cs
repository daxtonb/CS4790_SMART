using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Smart.Data;
using Smart.Data.Models;

namespace Smart.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly Smart.Data.ApplicationDbContext _context;

        public IndexModel(Smart.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public string FirstSort { get; set; }
        public string LastSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }


        public PaginatedList<Student> Student { get;set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            FirstSort = String.IsNullOrEmpty(sortOrder) ? "first_sort" : "";
            LastSort = sortOrder == "Last Name" ? "last_sort" : "Last Name";
            if(searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<Student> studentsIQ = from s in _context.Students.Where(n => n.StudentStatus.Description == "Active")
                                             select s;

            if(!String.IsNullOrEmpty(searchString))
            {
                studentsIQ = studentsIQ.Where(s => s.LastName.Contains(searchString) || s.FirstName.Contains(searchString));
            }

            switch(sortOrder)
            {
                case "last_sort":
                    studentsIQ = studentsIQ.OrderBy(s => s.LastName);
                    break;
                case "Last Name":
                    studentsIQ = studentsIQ.OrderBy(s => s.LastName);
                    break;
                default:
                    studentsIQ = studentsIQ.OrderBy(s => s.FirstName);
                    break;
            }

            int pageSize = 7;
            Student = await PaginatedList<Student>.CreateAsync(
                studentsIQ.AsNoTracking(), pageIndex ?? 1, pageSize);


            /*
            Student = await _context.Students
                .Include(s => s.StudentStatus).ToListAsync();*/

        }//testing
    }
}
