using Microsoft.AspNetCore.Mvc;
using Project_GradeBook_Web.Controllers;
using Project_GradeBook_Web.DTOs;
using Project_GradeBook_Web.Filters;
using Project_GradeBook_Web.Services;

namespace Project_GradeBook_Web.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class MarkController : ControllerBase
    {
        private readonly IMarkService markService;

        public MarkController(IMarkService markService)
        {
            this.markService = markService;
        }

        [HttpPost("{studentId}/marks")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddMark(int studentId, CreateMarkDto markDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await markService.AddMarkAsync(studentId, markDto);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Gets all marks for a student.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        [HttpGet("{studentId}/marks")]
        [ProducesResponseType(typeof(IEnumerable<MarkDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStudentMarks(int studentId)
        {
            try
            {
                var marks = await markService.GetStudentMarksAsync(studentId);

                if (marks == null || !marks.Any())
                {
                    return Ok(new List<MarkDto>());
                }

                return Ok(marks);
            }
            catch (IdNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Gets all marks for a student in a specific subject.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <param name="subjectId">The ID of the subject.</param>
        /// <returns>A list of marks for the student in the specified subject.</returns>
        [HttpGet("{studentId}/marks/subject/{subjectId}")]
        [ProducesResponseType(typeof(IEnumerable<MarkDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStudentMarksBySubject(int studentId, int subjectId)
        {
            try
            {
                var marks = await markService.GetStudentMarksBySubjectAsync(studentId, subjectId);

                if (marks == null || !marks.Any())
                {
                    return Ok(new List<MarkDto>());
                }

                return Ok(marks);
            }
            catch (IdNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Gets the average mark for a student in a specific subject.
        /// </summary>
        /// <param name="studentId">The ID of the student.</param>
        /// <param name="subjectId">The ID of the subject.</param>
        /// <returns>The average mark for the student in the specified subject.</returns>
        [HttpGet("{studentId}/average/subject/{subjectId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStudentAverageMarkBySubject(int studentId, int subjectId)
        {
            try
            {
                var average = await markService.GetStudentAverageMarkBySubjectAsync(studentId, subjectId);
                return Ok(average);
            }
            catch (IdNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (NoMarksFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Gets students ordered by their average mark.
        /// </summary>
        /// <param name="ascending">Whether to order the results in ascending order.</param>
        /// <returns>A list of students ordered by their average mark.</returns>
        [HttpGet("average")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStudentsOrderedByAverageMark([FromQuery] bool ascending = true)
        {
            var students = await markService.GetStudentsOrderedByAverageMarkAsync(ascending);
            return Ok(students);
        }
    }
}
