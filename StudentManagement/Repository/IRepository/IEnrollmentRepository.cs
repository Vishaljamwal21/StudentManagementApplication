using System.Collections.Generic;
using StudentManagementSystum.Models;

namespace StudentManagementSystum.Repository.IRepository
{
    public interface IEnrollmentRepository
    {
        ICollection<Enrollment> GetEnrollments();
        Enrollment GetEnrollment(int enrollmentId);
        bool EnrollmentExists(int enrollmentId);
        bool CreateEnrollment(Enrollment enrollment);
        bool UpdateEnrollment(Enrollment enrollment);
        bool DeleteEnrollment(Enrollment enrollment);
        ICollection<Enrollment> GetEnrollmentsByStudentId(int studentId);
        ICollection<Enrollment> GetEnrollmentsByCourseId(int courseId);
        bool Save();
    }
}
