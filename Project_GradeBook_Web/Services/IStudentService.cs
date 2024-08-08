﻿using Project_GradeBook_Web.DTOs;

namespace Project_GradeBook_Web.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentDto>> GetAllStudentsAsync();
        Task<StudentDto?> GetStudentByIdAsync(int id);
        Task<StudentDto> CreateStudentAsync(CreateStudentDto createStudentDto);
        Task UpdateStudentAsync(int id, CreateStudentDto updateStudentDto);
        Task<(int Id, string FirstName, string LastName)> DeleteStudentAsync(int id, bool deleteAddress);
        Task<IEnumerable<StudentAverageDto>> GetStudentsOrderedByAverageMarkAsync(bool ascending);

    }

}
