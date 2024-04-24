using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystum.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId {  get; set; }
        public int CourseId {  get; set; }  
        public DateTime EnrollmentDate { get; set; }
        [ForeignKey("StudentId")]
        public Student Student { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }
    }
}
