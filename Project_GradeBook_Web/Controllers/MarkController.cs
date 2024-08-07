using Microsoft.AspNetCore.Mvc;
using Project_GradeBook_Web.Controllers;
using Project_GradeBook_Web.DTOs;
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
        public async Task<IActionResult> AddMark(int studentId, CreateMarkDto markDto)
        {
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

        [HttpGet("{studentId}/marks")]
        public async Task<IActionResult> GetStudentMarks(int studentId)
        {
            var marks = await markService.GetStudentMarksAsync(studentId);
            return Ok(marks);
        }

        [HttpGet("{studentId}/marks/subject/{subjectId}")]
        public async Task<IActionResult> GetStudentMarksBySubject(int studentId, int subjectId)
        {
            var marks = await markService.GetStudentMarksBySubjectAsync(studentId, subjectId);
            return Ok(marks);
        }

        [HttpGet("{studentId}/average/subject/{subjectId}")]
        public async Task<IActionResult> GetStudentAverageMarkBySubject(int studentId, int subjectId)
        {
            var average = await markService.GetStudentAverageMarkBySubjectAsync(studentId, subjectId);
            return Ok(average);
        }

        [HttpGet("average")]
        public async Task<IActionResult> GetStudentsOrderedByAverageMark([FromQuery] bool ascending = true)
        {
            var students = await markService.GetStudentsOrderedByAverageMarkAsync(ascending);
            return Ok(students);
        }
    }
}
