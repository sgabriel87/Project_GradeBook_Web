namespace Project_GradeBook_Web.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }

    }
}
