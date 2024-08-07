using Project_GradeBook_Web.DTOs;

namespace Project_GradeBook_Web.Services
{
    public interface IAddressService
    {
        Task<AddressDto> GetStudentAddressAsync(int studentId);
        Task UpdateStudentAddressAsync(int id, CreateAddressDto addressDto);
    }
}
