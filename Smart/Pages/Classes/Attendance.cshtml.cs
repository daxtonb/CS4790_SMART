﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Smart.Data;
using Smart.Data.Models;

namespace Smart.Pages.Classes
{
    public class AttendanceModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        //private IHostingEnvironment _environment;// me 

        //public AttendanceModel(IHostingEnvironment environment)//me
        //{
        //    _environment = environment; //me
        //}

       
        public IEnumerable<AttendanceVieModel> Attendances { get; set; }

        public AttendanceModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(int classId)
        {
            var @class = await _context.Classes
                .Include(c => c.Attendances)
                .Include(c => c.Course)
                .Include(c => c.Term)
                .Include(c => c.ClassSchedules).ThenInclude(c => c.ScheduleAvailability)
                .FirstOrDefaultAsync(c => c.ClassId == classId);

            Attendances = @class.Attendances.GroupBy(a => a.Date.Date, a => a).Select(a => new AttendanceVieModel
            {
                Date = a.Key,
                OnTimeCount = a.Count(x => x.AttendanceStatusId == AttendanceStatusEnum.OnTime),
                LateCount = a.Count(x => x.AttendanceStatusId == AttendanceStatusEnum.Late),
                AbsentCount = a.Count(x => x.AttendanceStatusId == AttendanceStatusEnum.Absent)
            }).OrderByDescending(a => a.Date);

            ViewData["ClassTitle"] = $"{@class.Course.Name} - {@class.Term.TimeOfYear} {@class.Term.StartDate.Year}";
            ViewData["ClassSubtitle"] = ClassSchedule.GetScheduleString(@class.ClassSchedules.OrderBy(c => c.ScheduleAvailability.DayOfWeek));
            ViewData["ClassId"] = @class.ClassId;
        }

        //[BindProperty] //me
        //public IFormFile UploadCsv { get; set; } //me
        // on Post uploding file
        public async Task<PageResult> OnPostUploadCsvAsync()
        {
            var file = Request.Form.Files[0];

            //var file = Path.Combine(_environment.ContentRootPath, "uploads", UploadCsv.FileName);
            //using (var fileStream = new FileStream(file, FileMode.Create))
            //{
            //    await UploadCsv.CopyToAsync(fileStream);
            //}
            return Page();
        }

        public class AttendanceVieModel
        {
            public DateTime Date { get; set; }
            public int OnTimeCount { get; set; }
            public int LateCount { get; set; }
            public int AbsentCount { get; set; }
        }
    }
}