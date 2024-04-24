using StudentManagementSystum.Models;

namespace StudentManagementSystum.Repository.IRepository
{
    public interface ICourseRepository
    {
        ICollection<Course> GetCourse();
        Course GetCourse(int Courseid);
        bool CourseExists(int Courseid);
        bool CourseExists(string courseTitle);
        bool Createcourse(Course course);
        bool Updatecourse(Course course);
        bool Deletecourse(Course course);
        bool Save();
    }
}
