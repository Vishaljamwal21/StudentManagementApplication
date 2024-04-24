using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystum.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public string GradeValue { get; set; }
        public int EnrollmentId { get; set; }
        [ForeignKey("EnrollmentId")]
        public Enrollment Enrollment { get; set; }

        //[NotMapped]
        //public int StudentId => Enrollment?.StudentId ?? 0;

        //[NotMapped]
        //public int CourseId => Enrollment?.CourseId ?? 0;
    }
}

