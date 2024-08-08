﻿using Project_GradeBook_Web.DTOs;

namespace Project_GradeBook_Web.Services
{
    public interface ISubjectService
    {
        Task<string> AddSubjectAsync(CreateSubjectDto subjectDto);
        Task<IEnumerable<SubjectDto>> GetAllSubjectsAsync();
        Task UpdateSubjectAsync(int id, UpdateSubjectDto subjectDto);
        Task DeleteSubjectAsync(int id);
        
    }
}
