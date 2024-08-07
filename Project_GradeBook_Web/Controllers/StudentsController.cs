using Microsoft.AspNetCore.Mvc;
using Project_GradeBook_Web.DbContext;
using Project_GradeBook_Web.DTOs;
using Project_GradeBook_Web.Models;
using Project_GradeBook_Web.Services;

namespace Project_GradeBook_Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService studentService;

        public StudentController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(int id)
        {
            var student = await studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateStudentDto createStudentDto)
        {
            try
            {
                var student = await studentService.CreateStudentAsync(createStudentDto);
                return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // Return 400 Bad Request with validation error message
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message); // Return 409 Conflict with duplicate entry error message
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, CreateStudentDto updateStudentDto)
        {
            await studentService.UpdateStudentAsync(id, updateStudentDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id, [FromQuery] bool deleteAddress)
        {
            await studentService.DeleteStudentAsync(id, deleteAddress);
            return NoContent();
        }
    }
}
