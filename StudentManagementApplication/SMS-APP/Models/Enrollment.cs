using System.ComponentModel.DataAnnotations.Schema;

namespace SMS_APP.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        [ForeignKey("StudentId")]
        public Student Student { get; set; }
        [ForeignKey("CourseId")]
        public Course Course { get; set; }
        [NotMapped]
        public List<Course> CourseList { get; set; }
    }
}
