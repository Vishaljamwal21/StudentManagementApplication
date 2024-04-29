using System.Collections.Generic;
using StudentManagementSystum.Models;

namespace StudentManagementSystum.Repository.IRepository
{
    public interface IGradeRepository
    {
        ICollection<Grade> GetGrades();
        Grade GetGrade(int gradeId);
        bool GradeExists(int gradeId);
        bool CreateGrade(Grade grade);
        bool UpdateGrade(Grade grade);
        bool DeleteGrade(Grade grade);
        Task<Grade> GetByEnrollmentIdAsync(int enrollmentId);
        bool Save();
    }
}
