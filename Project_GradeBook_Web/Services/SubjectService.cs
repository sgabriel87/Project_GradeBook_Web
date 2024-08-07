using Microsoft.EntityFrameworkCore;
using Project_GradeBook_Web.DbContext;
using Project_GradeBook_Web.DTOs;
using Project_GradeBook_Web.Models;

namespace Project_GradeBook_Web.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly ApplicationDbContext ctx;

        public SubjectService(ApplicationDbContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<string?> AddSubjectAsync(CreateSubjectDto subjectDto)
        {
            var existingSubject = await ctx.Subjects
                .Where(s => s.Name == subjectDto.Name)
                .FirstOrDefaultAsync();

            if (existingSubject != null)
            {
                return $"Subject exists with id {existingSubject.Id}";
            }

            var newSubject = new Subject
            {
                Name = subjectDto.Name
            };

            ctx.Subjects.Add(newSubject);
            await ctx.SaveChangesAsync();

            return null;
        }
    }
}
