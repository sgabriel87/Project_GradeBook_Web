using System.ComponentModel.DataAnnotations;

namespace Project_GradeBook_Web.DTOs
{
    public class CreateMarkDto
    {
        [Range(1, 10)]
        public int Value { get; set; }
        public int SubjectId { get; set; }
    }
}
