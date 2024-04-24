using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystum.Models.DTO
{
    public class GradeDTO
    {
        public int Id { get; set; }
        public int EnrollmentId { get; set; } // Foreign key for Enrollment
        public string GradeValue { get; set; }

        [ForeignKey("EnrollmentId")]
        public Enrollment Enrollment { get; set; }
        //[NotMapped]
        //public int StudentId => Enrollment?.StudentId ?? 0;

        //[NotMapped]
        //public int CourseId => Enrollment?.CourseId ?? 0;
    }
}
