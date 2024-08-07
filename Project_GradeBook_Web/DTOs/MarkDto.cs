namespace Project_GradeBook_Web.DTOs
{
    public class MarkDto
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public DateTime DateAwarded { get; set; }
        public SubjectDto Subject { get; set; }
    }
}
