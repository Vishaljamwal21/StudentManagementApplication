using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystum.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string City {  get; set; }
        public string State {  get; set; }
    }
}
