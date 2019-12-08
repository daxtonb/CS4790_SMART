using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Smart.Data;

namespace MyApp.Namespace
{
    public class EventsViewModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public List<Smart.Data.Models.Event> MyEvents {get; set;}

        public EventsViewModel(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }
        
        public async Task OnGetAsync()
        {
            MyEvents = await _db.Events.OrderByDescending(i => i.DateTime).ToListAsync();
        }
    }
}