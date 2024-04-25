using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystum.Data;
using StudentManagementSystum.Models;
using StudentManagementSystum.Repository.IRepository;

namespace StudentManagementSystum.Repository
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly ApplicationDbContext _context;

        public EnrollmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CreateEnrollment(Enrollment enrollment)
        {
            _context.Enrollments.Add(enrollment);
            return Save();
        }

        public bool DeleteEnrollment(Enrollment enrollment)
        {
            _context.Enrollments.Remove(enrollment);
            return Save();
        }

        public Enrollment GetEnrollment(int enrollmentId)
        {
            try
            {
                return _context.Enrollments
                    .Include(e => e.Student)
                    .Include(e => e.Course)
                    .FirstOrDefault(e => e.Id == enrollmentId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }




        public ICollection<Enrollment> GetEnrollments()
        {
            // Include Student and Course navigation properties
            return _context.Enrollments
                .Include(e => e.Student)
                .Include(e => e.Course)
                .ToList();
        }


        public ICollection<Enrollment> GetEnrollmentsByCourseId(int courseId)
        {
            return _context.Enrollments.Where(e => e.CourseId == courseId).ToList();
        }

        public ICollection<Enrollment> GetEnrollmentsByStudentId(int studentId)
        {
            return _context.Enrollments.Where(e => e.StudentId == studentId).ToList();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool UpdateEnrollment(Enrollment enrollment)
        {
            _context.Enrollments.Update(enrollment);
            return Save();
        }

        public bool EnrollmentExists(int enrollmentId)
        {
            return _context.Enrollments.Any(e => e.Id == enrollmentId);
        }
    }
}
