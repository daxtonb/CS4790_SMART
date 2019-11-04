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
    public class ErrorsViewModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public List<Smart.Data.Models.Error> MyErrors {get; set;}
        public ErrorsViewModel(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task OnGetAsync()
        {
            MyErrors = await _db.Errors.ToListAsync();
        }
    }
}