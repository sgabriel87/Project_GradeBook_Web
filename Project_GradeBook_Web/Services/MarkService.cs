using Microsoft.EntityFrameworkCore;
using Project_GradeBook_Web.DbContext;
using Project_GradeBook_Web.DTOs;
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

        public async Task AddMarkAsync(int studentId, CreateMarkDto markDto)
        {
            var mostRecentMark = await ctx.Marks
                .Where(m => m.StudentId == studentId)
                .OrderByDescending(m => m.DateAwarded)
                .FirstOrDefaultAsync();

            if (mostRecentMark != null)
            {
                var newMarkDate = DateTime.UtcNow;
                var timeDifference = newMarkDate - mostRecentMark.DateAwarded;

                if (timeDifference.TotalSeconds < 10)
                {
                    throw new InvalidOperationException($"Mark cannot be awarded. It must be at least 10 seconds after the previous mark with id {mostRecentMark.Id}.");
                }
            }

            var mark = new Mark
            {
                StudentId = studentId,
                SubjectId = markDto.SubjectId,
                Value = markDto.Value,
                DateAwarded = DateTime.UtcNow
            };

            ctx.Marks.Add(mark);
            await ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<MarkDto>> GetStudentMarksAsync(int studentId)
        {
            return await ctx.Marks
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
        }

        public async Task<IEnumerable<MarkDto>> GetStudentMarksBySubjectAsync(int studentId, int subjectId)
        {
            return await ctx.Marks
                .Where(m => m.StudentId == studentId && m.SubjectId == subjectId)
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
        }

        public async Task<double> GetStudentAverageMarkBySubjectAsync(int studentId, int subjectId)
        {
            var average = await ctx.Marks
                .Where(m => m.StudentId == studentId && m.SubjectId == subjectId)
                .AverageAsync(m => m.Value);

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

