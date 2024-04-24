using StudentManagementSystum.Data;
using StudentManagementSystum.Models;
using StudentManagementSystum.Repository.IRepository;

namespace StudentManagementSystum.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Createstudent(Student student)
        {
            _context.Students.Add(student);
            return Save();
        }

        public bool Deletestudent(Student student)
        {
            _context.Students.Remove(student);
            return Save();
        }

        public ICollection<Student> GetStudent()
        {
            return _context.Students.ToList();
        }

        public Student GetStudent(int studentid)
        {
            return _context.Students.Find(studentid);
        }

        public bool Save()
        {
            return _context.SaveChanges() == 1 ? true : false;
        }

        public bool StudentExists(int studentid)
        {
            return _context.Students.Any(st => st.Id == studentid);
        }

        public bool StudentExists(string studentname)
        {
            return _context.Students.Any(st => st.Name == studentname);
        }

        public bool Updatestudent(Student student)
        {
            _context.Students.Update(student);
            return Save();
        }
    }
}
