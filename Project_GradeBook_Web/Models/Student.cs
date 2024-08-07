using System.Net;

namespace Project_GradeBook_Web.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public int? AddressId { get; set; }  
        public Address Address { get; set; } 
        public ICollection<Mark> Marks { get; set; }
    }
}
