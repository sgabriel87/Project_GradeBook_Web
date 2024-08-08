using Microsoft.AspNetCore.Mvc;
using Project_GradeBook_Web.DTOs;
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
        /// <param name="subjectDto"></param>
        /// <returns></returns>
        [HttpPost("subjects")]
        public async Task<IActionResult> AddSubject(CreateSubjectDto subjectDto)
        {
            var result = await subjectService.AddSubjectAsync(subjectDto);

            if (result != null)
            {
                return Conflict(result);
            }

            return NoContent();
        }
    }
}
