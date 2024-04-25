using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystum.Models.DTO
{
    public class GradeDTO
    {
        public int Id { get; set; }
        public int EnrollmentId { get; set; } 
        public string GradeValue { get; set; }

        [ForeignKey("EnrollmentId")]
        public Enrollment Enrollment { get; set; }
    }
}
