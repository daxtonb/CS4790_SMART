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
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Student MyStudent { get; set; }
        public List<StudentMeeting> StudentMeetings {get; set;}
        public List<Term> Terms { get; set; }
        public List<Meeting> Meetings {get; set;}
        public List<School> PublicSchools {get; set;}
        public School SmartSchool {get; set;}

        public IndexModel(ApplicationDbContext dbContext)
        {
            _db = dbContext;
        }

        public async Task OnGetAsync(int studentId)
        {
            MyStudent = await _db.Students.FindAsync(studentId);
            Terms = await _db.Terms.ToListAsync();
            PublicSchools = await _db.Schools.Where(i => !(i.Name.Contains("No Poor Among Us"))).ToListAsync();
            SmartSchool = await _db.Schools.Where(i => i.Name.Contains("No Poor Among Us")).FirstOrDefaultAsync();

            StudentMeetings = await _db.StudentMeetings
                                        .Include(i => i.Meeting)
                                        .ThenInclude(i => i.Class)
                                        .ThenInclude(i => i.Course)
                                        .ThenInclude(i => i.School)
                                        .ThenInclude(i => i.ScheduleAvailabilities)
                                        .Include(i => i.Meeting)
                                        .ThenInclude(i => i.Class)
                                        .ThenInclude(i => i.InstructorUser)
                                        .Where(i => i.StudentId == studentId)
                                        .ToListAsync();

            // Meetings = await _db.Meetings.Include(i => i.Class)
            //                         .ThenInclude(c => c.Course)
            //                         .ThenInclude(s => s.School)
            //                         .ThenInclude(i => i.ScheduleAvailabilities).ToListAsync();
            Meetings = await _db.Meetings.Include(i => i.Class)
                                            .ThenInclude(c => c.Course)
                                            .ThenInclude(c => c.School)
                                            .ThenInclude(i => i.ScheduleAvailabilities)
                                            .Include(i => i.Class)
                                            .ThenInclude(i => i.InstructorUser)
                                            .Include(i => i.Class)
                                            .ThenInclude(i => i.Term)
                                            .Include(i => i.StudentMeetings)
                                            .ToListAsync();

            foreach(var meeting in StudentMeetings)
            {
                var matchingMeeting = Meetings.Where(i => i.MeetingId == meeting.MeetingId).FirstOrDefault();

                if(matchingMeeting != null)
                {
                    Meetings.Remove(matchingMeeting);
                }
            }
        }
        
        public async Task<IActionResult> OnPostAsync(int meetingId, int studentId, int? delete)
        {
            Console.WriteLine("Posting Request Method: " + Request.Method);

            if(delete != null)
            {
                Console.WriteLine("Reached Delete Function");
                   var newMeeting = new StudentMeeting(){
                        StudentId = studentId,
                       MeetingId = meetingId,
                       Meeting = await _db.Meetings.Where(i => i.MeetingId == meetingId).FirstOrDefaultAsync(),
                       Student = await _db.Students.Where(i => i.StudentId == studentId).FirstOrDefaultAsync()
                   };
                   _db.StudentMeetings.Remove(newMeeting);
                   await _db.SaveChangesAsync();
                   return RedirectToPage(new {studentId = studentId});
            }
            else
            {
                if(ModelState.IsValid)
               {
                   var newMeeting = new StudentMeeting(){
                        StudentId = studentId,
                       MeetingId = meetingId,
                       Meeting = await _db.Meetings.Where(i => i.MeetingId == meetingId).FirstOrDefaultAsync(),
                       Student = await _db.Students.Where(i => i.StudentId == studentId).FirstOrDefaultAsync()
                   };
                   _db.StudentMeetings.Add(newMeeting);
                   await _db.SaveChangesAsync();
                   return RedirectToPage(new {studentId = studentId});
               }  
            }
               

            return RedirectToPage(new {studentId = studentId});     
        }
    }
}