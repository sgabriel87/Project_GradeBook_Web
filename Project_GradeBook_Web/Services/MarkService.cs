using Microsoft.EntityFrameworkCore;
using Project_GradeBook_Web.DbContext;
using Project_GradeBook_Web.DTOs;
using Project_GradeBook_Web.Filters;
using Project_GradeBook_Web.Models;

namespace Project_GradeBook_Web.Services
{
    public class MarkService : IMarkService
    {
        private readonly ApplicationDbContext ctx;

        public MarkService(ApplicationDbContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<string> AddMarkAsync(int studentId, CreateMarkDto markDto)
        {
            var student = await ctx.Students.FirstOrDefaultAsync(s => s.Id == studentId);
            if (student == null)
            {
                return $"Student with ID {studentId} not found.";
            }

            var subject = await ctx.Subjects.FirstOrDefaultAsync(s => s.Id == markDto.SubjectId);
            if (subject == null)
            {
                return $"Subject with ID {markDto.SubjectId} not found.";
            }

            var newMark = new Mark
            {
                StudentId = studentId,
                SubjectId = markDto.SubjectId,
                Value = markDto.Value,
                DateAwarded = DateTime.UtcNow
            };

            ctx.Marks.Add(newMark);
            await ctx.SaveChangesAsync();

            return $"Mark added successfully. Student ID: {studentId}, Mark: {markDto.Value}, Subject: {subject.Name}";
        }

        public async Task<IEnumerable<MarkDto>> GetStudentMarksAsync(int studentId)
        {
            var studentExists = await ctx.Students.AnyAsync(s => s.Id == studentId);
            if (!studentExists)
            {
                throw new IdNotFoundException($"Student with ID {studentId} not found.");
            }

            var marks = await ctx.Marks
                .Where(m => m.StudentId == studentId)
                .Select(m => new MarkDto
                {
                    Id = m.Id,
                    Value = m.Value,
                    DateAwarded = m.DateAwarded,
                    Subject = new SubjectDto
                    {
                        Id = m.Subject.Id,
                        Name = m.Subject.Name
                    }
                })
                .ToListAsync();

            if (!marks.Any())
            {
                return new List<MarkDto>();
            }

            return marks;
        }

        public async Task<IEnumerable<MarkDto>> GetStudentMarksBySubjectAsync(int studentId, int subjectId)
        {
            var studentExists = await ctx.Students.AnyAsync(s => s.Id == studentId);
            if (!studentExists)
            {
                throw new IdNotFoundException($"Student with ID {studentId} not found.");
            }

            var marks = await ctx.Marks
                .Where(m => m.StudentId == studentId && m.SubjectId == subjectId)
                .Select(m => new MarkDto
                {
                    Id = m.Id,
                    Value = (int)m.Value,
                    DateAwarded = m.DateAwarded,
                    Subject = new SubjectDto
                    {
                        Id = m.Subject.Id,
                        Name = m.Subject.Name
                    }
                })
                .ToListAsync();

            if (!marks.Any())
            {
                return new List<MarkDto>();
            }

            return marks;
        }

        public async Task<double?> GetStudentAverageMarkBySubjectAsync(int studentId, int subjectId)
        {
            var studentExists = await ctx.Students.AnyAsync(s => s.Id == studentId);
            if (!studentExists)
            {
                throw new IdNotFoundException($"Student with ID {studentId} not found.");
            }

            var subjectExists = await ctx.Subjects.AnyAsync(s => s.Id == subjectId);
            if (!subjectExists)
            {
                throw new IdNotFoundException($"Subject with ID {subjectId} not found.");
            }

            var average = await ctx.Marks
                .Where(m => m.StudentId == studentId && m.SubjectId == subjectId)
                .AverageAsync(m => m.Value);

            if (double.IsNaN(average))
            {
                throw new NoMarksFoundException($"No marks found for student with ID {studentId} in subject with ID {subjectId}.");
            }

            return average;
        }

        public async Task<IEnumerable<StudentAverageDto>> GetStudentsOrderedByAverageMarkAsync(bool ascending)
        {
            var students = await ctx.Students
                .Select(s => new
                {
                    s.Id,
                    s.FirstName,
                    s.LastName,
                    AverageMark = s.Marks.Any() ? (double?)s.Marks.Average(m => m.Value) : null
                })
                .ToListAsync();

            var orderedStudents = ascending
                ? students.OrderBy(s => s.AverageMark ?? double.MaxValue).ToList()
                : students.OrderByDescending(s => s.AverageMark ?? double.MinValue).ToList();

            return orderedStudents.Select(s => new StudentAverageDto
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                AverageGrade = s.AverageMark ?? 0
            });
        }
    }
}

