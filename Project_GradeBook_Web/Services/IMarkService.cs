﻿using Project_GradeBook_Web.DTOs;

namespace Project_GradeBook_Web.Services
{
    public interface IMarkService
    {
        Task AddMarkAsync(int studentId, CreateMarkDto markDto);
        Task<IEnumerable<MarkDto>> GetStudentMarksAsync(int studentId);
        Task<IEnumerable<MarkDto>> GetStudentMarksBySubjectAsync(int studentId, int subjectId);
        Task<double> GetStudentAverageMarkBySubjectAsync(int studentId, int subjectId);
        Task<IEnumerable<StudentAverageDto>> GetStudentsOrderedByAverageMarkAsync(bool ascending);
    }
}
