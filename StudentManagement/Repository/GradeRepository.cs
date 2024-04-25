using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystum.Data;
using StudentManagementSystum.Models;
using StudentManagementSystum.Repository.IRepository;

namespace StudentManagementSystum.Repository
{
    public class GradeRepository : IGradeRepository
    {
        private readonly ApplicationDbContext _context;

        public GradeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool CreateGrade(Grade grade)
        {
            var enrollmentId = grade.EnrollmentId;
            var enrollment = _context.Enrollments.FirstOrDefault(e => e.Id == enrollmentId);
            if (enrollment == null)
            {
                return false;
            }
            grade.Enrollment = null;
            _context.Grades.Add(grade);
            return Save();
        }



        public bool DeleteGrade(Grade grade)
        {
            _context.Grades.Remove(grade);
            return Save();
        }

        public Grade GetGrade(int gradeId)
        {
            return _context.Grades
                .Include(g => g.Enrollment)
                    .ThenInclude(e => e.Student)
                .Include(g => g.Enrollment)
                    .ThenInclude(e => e.Course)
                .FirstOrDefault(g => g.Id == gradeId);
        }

        public ICollection<Grade> GetGrades()
        {
            return _context.Grades
                .Include(g => g.Enrollment)
                    .ThenInclude(e => e.Student)
                .Include(g => g.Enrollment)
                    .ThenInclude(e => e.Course)
                .ToList();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public bool UpdateGrade(Grade grade)
        {
            _context.Grades.Update(grade);
            return Save();
        }

        public bool GradeExists(int gradeId)
        {
            return _context.Grades.Any(g => g.Id == gradeId);
        }

    }
}
