using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Smart.Data;
using Smart.Data.Models;

namespace Smart.Pages.Students
{
    public class EditModel : PageModel
    {
        private readonly Smart.Data.ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        public int studentIdentification;

        public EditModel(Smart.Data.ApplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        [BindProperty]
        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            studentIdentification = id;
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Students
                .Include(s => s.StudentStatus).FirstOrDefaultAsync(m => m.StudentId == id);

            if (Student == null)
            {
                return NotFound();
            }
           ViewData["StudentStatusId"] = new SelectList(_context.StudentStatuses, "StudentStatusId", "Description");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var student = await _context.Students.FindAsync(Student.StudentId);
          
            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            if (files.Count > 0)
            {
                var uploads = Path.Combine(webRootPath, "images");
                var extension = Path.GetExtension(files[0].FileName);

                var ex = student.Photo;

                if (student.Photo != null)
                {
                    var imagePath = Path.Combine(webRootPath, student.Photo.TrimStart('\\'));

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }
                }
                          
                using (var fileStream = new FileStream(Path.Combine(uploads, Student.StudentId + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                
                student.Photo = @"\images\" + Student.StudentId + extension;
            }
            student.FirstName = Student.FirstName;
            student.LastName = Student.LastName;
            student.DateOfBirth = Student.DateOfBirth;
            student.Address = Student.Address;
            student.Village = Student.Village;
            student.LocationLattitude = Student.LocationLattitude;
            student.LocationLongitude = Student.LocationLongitude;
            student.PublicSchoolLevel = Student.PublicSchoolLevel;
            student.EnglishLevel = Student.EnglishLevel;
            student.ItLevel = Student.ItLevel;
            student.GuardianName = Student.GuardianName;
            student.Phone = Student.Phone;
            student.StudentStatusId = Student.StudentStatusId;
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
    }
}
