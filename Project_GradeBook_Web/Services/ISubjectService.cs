using Project_GradeBook_Web.DTOs;

namespace Project_GradeBook_Web.Services
{
    public interface ISubjectService
    {
        Task<string> AddSubjectAsync(CreateSubjectDto subjectDto);
    }
}
