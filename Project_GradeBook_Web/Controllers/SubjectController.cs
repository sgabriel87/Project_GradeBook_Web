using Microsoft.AspNetCore.Mvc;
using Project_GradeBook_Web.DTOs;
using Project_GradeBook_Web.Filters;
using Project_GradeBook_Web.Services;

namespace Project_GradeBook_Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            this.subjectService = subjectService;
        }

        /// <summary>
        /// Adds a new subject to the database.
        /// </summary>
        /// <param name="subjectDto">The details of the subject to add.</param>
        /// <returns>Returns no content if successful, or a conflict if the subject already exists.</returns>
        [HttpPost]
        public async Task<IActionResult> AddSubject(CreateSubjectDto subjectDto)
        {
            var result = await subjectService.AddSubjectAsync(subjectDto);

            if (result != null)
            {
                return Conflict(result);
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes a subject by its ID.
        /// </summary>
        /// <param name="id">The ID of the subject to delete.</param>
        /// <returns>No content if the subject is successfully deleted, or not found if the subject does not exist.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            try
            {
                await subjectService.DeleteSubjectAsync(id);
                return NoContent();
            }
            catch (IdNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Updates the details of a subject.
        /// </summary>
        /// <param name="id">The ID of the subject to update.</param>
        /// <param name="subjectDto">The updated details of the subject.</param>
        /// <returns>No content if the update is successful, or not found if the subject does not exist.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(int id, UpdateSubjectDto subjectDto)
        {
            try
            {
                await subjectService.UpdateSubjectAsync(id, subjectDto);
                return NoContent();
            }
            catch (IdNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Gets a list of all subjects.
        /// </summary>
        /// <returns>A list of all subjects.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllSubjects()
        {
            var subjects = await subjectService.GetAllSubjectsAsync();
            return Ok(subjects);
        }
    }
}
