using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Smart.Data;
using Smart.Data.Models;

namespace MyApp.Namespace
{
    public class ErrorsModel : PageModel
    {
        ApplicationDbContext _db;
        public List<Error> MyErrors {get; set;}

        public ErrorsModel(ApplicationDbContext db)
        {
            this._db = db;
        }

        public async Task OnGetAsync()
        {
            MyErrors = await _db.Errors.ToListAsync();
        }
    }
}