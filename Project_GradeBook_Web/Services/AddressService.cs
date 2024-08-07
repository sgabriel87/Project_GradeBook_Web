using Microsoft.EntityFrameworkCore;
using Project_GradeBook_Web.DbContext;
using Project_GradeBook_Web.DTOs;
using Project_GradeBook_Web.Models;

namespace Project_GradeBook_Web.Services
{
    public class AddressService : IAddressService
    {
        private readonly ApplicationDbContext ctx;

        public AddressService(ApplicationDbContext ctx)
        {
            this.ctx = ctx;
        }

        public async Task<AddressDto> GetStudentAddressAsync(int studentId)
        {
            var student = await ctx.Students.Include(s => s.Address).FirstOrDefaultAsync(s => s.Id == studentId);

            if (student == null)
            {
                return null;
            }

            if (student.Address == null)
            {
                return new AddressDto
                {
                    Id = 0,
                    City = "Student has no address.",
                    Street = string.Empty,
                    Number = string.Empty
                };
            }

            return new AddressDto
            {
                Id = student.Address.Id,
                City = student.Address.City,
                Street = student.Address.Street,
                Number = student.Address.Number
            };
        }

        public async Task UpdateStudentAddressAsync(int id, CreateAddressDto addressDto)
        {
            var student = await ctx.Students.Include(s => s.Address).FirstOrDefaultAsync(s => s.Id == id);
            if (student == null) throw new KeyNotFoundException("Student not found");

            if (student.Address == null)
            {
                student.Address = new Address
                {
                    City = addressDto.City,
                    Street = addressDto.Street,
                    Number = addressDto.Number
                };
            }
            else
            {
                student.Address.City = addressDto.City;
                student.Address.Street = addressDto.Street;
                student.Address.Number = addressDto.Number;
            }

            await ctx.SaveChangesAsync();
        }
    }
}
