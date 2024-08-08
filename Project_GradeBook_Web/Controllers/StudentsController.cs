using Microsoft.AspNetCore.Mvc;
using Project_GradeBook_Web.DTOs;
using Project_GradeBook_Web.Filters;
using Project_GradeBook_Web.Services;

namespace Project_GradeBook_Web.Controllers
{
    /// <summary>
    /// Controller for managing students.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService studentService;

        public StudentController(IStudentService studentService)
        {
            this.studentService = studentService;
        }

        /// <summary>
        /// Retrieves all students.
        /// </summary>
        /// <returns>A list of all students.</returns>
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        /// <summary>
        /// Retrieves a student by their ID.
        /// </summary>
        /// <param name="id">The ID of the student.</param>
        /// <returns>The student details.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStudentById(int id)
        {
            try
            {
                var student = await studentService.GetStudentByIdAsync(id);
                return Ok(student);
            }
            catch (IdNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
           
        }

        /// <summary>
        /// Creates a new student.
        /// </summary>
        /// <param name="createStudentDto">The details of the student to create.</param>
        /// <returns>The created student details.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreateStudent(CreateStudentDto createStudentDto)
        {
            try
            {
                var student = await studentService.CreateStudentAsync(createStudentDto);
                return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        /// <summary>
        /// Updates an existing student by ID.
        /// </summary>
        /// <param name="id">The ID of the student.</param>
        /// <param name="updateStudentDto">The updated student details.</param>
        /// <returns>No content if the update is successful.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateStudent(int id, CreateStudentDto updateStudentDto)
        {
            try
            {
                await studentService.UpdateStudentAsync(id, updateStudentDto);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (IdNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a student by ID.
        /// </summary>
        /// <param name="id">The ID of the student.</param>
        /// <param name="deleteAddress">Flag indicating whether to delete the student's address as well.</param>
        /// <returns>No content if the deletion is successful.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteStudent(int id, [FromQuery] bool deleteAddress)
        {
            try
            {
                var (studentId, firstName, lastName) = await studentService.DeleteStudentAsync(id, deleteAddress);
                var message = $"Student with ID {studentId} and name {firstName} {lastName} has been deleted.";
                return Ok(new { Message = message });
            }
            catch (IdNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
