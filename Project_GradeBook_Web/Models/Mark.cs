using System.ComponentModel.DataAnnotations.Schema;

namespace Project_GradeBook_Web.Models
{
    public class Mark
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTime DateAwarded { get; set; }
        [ForeignKey(nameof(SubjectId))]
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        [ForeignKey(nameof(StudentId))]
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
