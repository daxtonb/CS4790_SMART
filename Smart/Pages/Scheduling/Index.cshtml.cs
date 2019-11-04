using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Smart.Data;

namespace MyApp.Namespace
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public List<string> DaysOfTheWeek {get; set;} = new List<string>(){"Monday", "Tuesday", "Wednesday", "Thursday", "Friday"};
        
        public IndexModel(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task OnGetAsync(int? studentId)
        {
            
        }
    }
}