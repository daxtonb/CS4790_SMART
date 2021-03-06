﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Smart.Data;
using Smart.Data.Models;

namespace Smart.Pages.Classes
{
    public class AssessmentsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IEnumerable<Assessment> Assessments { get; set; }
        public List<AssessmentSubmissionViewModel> Submissions { get; set; }

        public AssessmentsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(int classId)
        {
            var @class = await _context.Classes
                .Include(c => c.Assessments).ThenInclude(a => a.StudentAssessments)
                .Include(c => c.Assessments).ThenInclude(a => a.AssessmentType)
                .Include(c => c.Course)
                .Include(c => c.Term)
                .Include(c => c.Meetings).ThenInclude(c => c.ScheduleAvailability)
                .FirstOrDefaultAsync(c => c.ClassId == classId);

            Assessments = @class.Assessments.OrderBy(a => a.Deadline);

            ViewData["ClassId"] = @class.ClassId;
        }

        public async Task<IActionResult> OnGetAssessmentFormAsync(int classId, int? assessmentId)
        {
            Assessment assessment;

            if (assessmentId.HasValue)
            {
                assessment = await _context.Assessments.FirstOrDefaultAsync(a => a.AssessmentId == assessmentId.Value);
                if (assessment == null)
                {
                    return NotFound();
                }
            }
            else
            {
                assessment = new Assessment() { ClassId = classId, Deadline = DateTime.Today.AddDays(1) };
            }

            ViewData["assessmentTypes"] = await _context.AssessmentTypes.ToListAsync();

            return new PartialViewResult()
            {
                ViewName = "_AssessmentForm",
                ViewData = new ViewDataDictionary<Assessment>(ViewData, assessment)
            };
        }

        public async Task<RedirectToPageResult> OnPostSaveAssessmentAsync(Assessment model)
        {
            if (ModelState.IsValid)
            {
                Assessment assessment = await _context.Assessments.FirstOrDefaultAsync(a => a.AssessmentId == model.AssessmentId);

                if (assessment == null)
                {
                    assessment = model;
                    _context.Assessments.Add(assessment);
                }
                else
                {
                    assessment.Deadline = model.Deadline;
                    assessment.Description = model.Description;
                    assessment.Title = model.Title;
                    assessment.PointsPossible = model.PointsPossible;
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToPage(new { classId = model.ClassId });
        }

        public async Task<IActionResult> OnGetSubmissions(int classId, int assessmentId)
        {
            var students = await _context.Meetings
                .Include(s => s.StudentMeetings).ThenInclude(m => m.Student).ThenInclude(m => m.StudentAssessments)
                .Where(s => s.ClassId == classId)
                .SelectMany(s => s.StudentMeetings.Select(m => m.Student))
                .Distinct()
                .ToListAsync();

            var assessment = await _context.Assessments
                .Include(a => a.StudentAssessments).ThenInclude(f => f.File)
                .FirstOrDefaultAsync(a => a.AssessmentId == assessmentId);

            if (assessment == null)
            {
                return NotFound();
            }

            Submissions = new List<AssessmentSubmissionViewModel>();

            foreach (var student in students)
            {
                // EF already linked up the StudentAssessment objects to their respective students.
                // We only queried for this assessment's submissions, so it's safe to grab the first one.
                var studentAssessment = student?.StudentAssessments?.FirstOrDefault();

                Submissions.Add(new AssessmentSubmissionViewModel()
                {
                    StudentId = student.StudentId,
                    StudentName = student.LastName + ", " + student.FirstName,
                    Comments = studentAssessment?.Comments,
                    FileName = studentAssessment?.File?.FileName,
                    FileId = studentAssessment?.FileId,
                    PointsAwarded = studentAssessment?.PointsAwarded,
                    SubmissionDateTime = studentAssessment?.SubmissionDateTime
                });
            }

            ViewData["ClassId"] = classId;
            ViewData["AssessmentId"] = assessmentId;

            return new PartialViewResult()
            {
                ViewName = "_AssessmentSubmissions",
                ViewData = new ViewDataDictionary<IEnumerable<AssessmentSubmissionViewModel>>(ViewData, Submissions)
            };
        }

        public async Task<IActionResult> OnGetStudentSubmissions(int classId, int studentId)
        {
            var studentAssessments = await _context.StudentAssessments
                .Include(s => s.Student)
                .Include(s => s.File)
                .Include(s => s.Assessment)
                .Where(s => s.Assessment.ClassId == classId && s.StudentId == studentId)
                .Select(s => new AssessmentSubmissionViewModel
                {
                    AssessmentId = s.AssessmentId,
                    AssessmentName = s.Assessment.Title,
                    Comments = s.Comments,
                    PointsAwarded = s.PointsAwarded,
                    PointsPossible = s.Assessment.PointsPossible,
                    StudentId = s.StudentId,
                    FileName = s.File != null ? s.File.FileName : null,
                    FileId = s.File != null ? s.FileId : (int?)null,
                    SubmissionDateTime = s.SubmissionDateTime,
                    StudentName = s.Student.LastName + ", " + s.Student.FirstName
                }).ToListAsync();

            var otherAssessments = await _context.Assessments
                .Where(a => a.ClassId == classId && !studentAssessments.Any(s => s.AssessmentId == a.AssessmentId))
                .Select(a => new AssessmentSubmissionViewModel
                {
                    AssessmentId = a.AssessmentId,
                    AssessmentName = a.Title,
                    PointsPossible = a.PointsPossible,
                    StudentId = studentId
                }).ToListAsync();

            return new PartialViewResult()
            {
                ViewName = "_StudentAssessmentSubmissions",
                ViewData = new ViewDataDictionary<IEnumerable<AssessmentSubmissionViewModel>>(ViewData, studentAssessments.Concat(otherAssessments).OrderByDescending(a => a.SubmissionDateTime))
            };
        }

        public async Task<IActionResult> OnPostSubmitStudentAssessment(AssessmentSubmissionViewModel model)
        {
            var studentAssessment = await _context.StudentAssessments
                .Include(a => a.File)
                .FirstOrDefaultAsync(a => a.AssessmentId == model.AssessmentId && a.StudentId == model.StudentId);

            if (studentAssessment == null)
            {
                studentAssessment = new StudentAssessment()
                {
                    AssessmentId = model.AssessmentId,
                    StudentId = model.StudentId,
                    Comments = model.Comments,
                    PointsAwarded = model.PointsAwarded.HasValue ? model.PointsAwarded.Value : 0,
                    SubmissionDateTime = model.SubmissionDateTime.HasValue ? model.SubmissionDateTime.Value : DateTime.Now
                };
                _context.StudentAssessments.Add(studentAssessment);
            }
            else
            {
                studentAssessment.Comments = model.Comments;
                studentAssessment.PointsAwarded = model.PointsAwarded.HasValue ? model.PointsAwarded.Value : 0;
                studentAssessment.SubmissionDateTime = model.SubmissionDateTime.HasValue ? model.SubmissionDateTime.Value : DateTime.Now;
            }

            if (Request.Form.Files.Any())
            {
                var file = Request.Form.Files.First();

                // CONDITION: This student already has a file uploaded for this assignment
                if (studentAssessment.File != null)
                {
                    studentAssessment.File.FileName = file.FileName;
                    studentAssessment.File.ByteData = await Data.Models.File.SerializeFileAsync(file);
                }
                else
                {
                    studentAssessment.File = new File()
                    {
                        FileTypeId = FileTypeEnum.Assessment,
                        FileName = file.FileName,
                        StudentId = model.StudentId,
                        ByteData = await Data.Models.File.SerializeFileAsync(file)
                    };
                }
            }

            await _context.SaveChangesAsync();
            return StatusCode(200);
        }

        public class AssessmentSubmissionViewModel
        {
            public int StudentId { get; set; }
            public int AssessmentId { get; set; }
            public string AssessmentName { get; set; }
            public string StudentName { get; set; }
            public int? PointsAwarded { get; set; }
            public int PointsPossible { get; set; }
            public int? FileId { get; set; }
            public string FileName { get; set; }
            public string Comments { get; set; }
            public DateTime? SubmissionDateTime { get; set; }
        }
    }
}