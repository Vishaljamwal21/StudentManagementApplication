using StudentManagementSystum.Models;

namespace StudentManagementSystum.Repository.IRepository
{
    public interface IStudentRepository
    {
        ICollection<Student> GetStudent();
        Student GetStudent(int studentid);
        bool StudentExists(int studentid);
        bool StudentExists(string studentname);
        bool Createstudent(Student student);
        bool Updatestudent(Student student);
        bool Deletestudent(Student student);
        ICollection<Student> GetStudentsByEmail(string email);
        Task<bool> IsUniqueEmail(string email);
        bool Save();
    }
}
