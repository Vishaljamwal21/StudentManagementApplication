using StudentManagementSystum.Controllers;
using StudentManagementSystum.Data;
using StudentManagementSystum.Models;
using StudentManagementSystum.Repository.IRepository;

namespace StudentManagementSystum.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;
        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool CourseExists(int Courseid)
        {
            return _context.Courses.Any(cu => cu.Id == Courseid);
        }

        public bool CourseExists(string courseTitle)
        {
            return _context.Courses.Any(cu => cu.Title == courseTitle);
        }

        public bool Createcourse(Course course)
        {
            _context.Courses.Add(course);
            return Save();
        }

        public bool Deletecourse(Course course)
        {
            _context.Courses.Remove(course);
            return Save();
        }

        public ICollection<Course> GetCourse()
        {
            return _context.Courses.ToList();
        }

        public Course GetCourse(int Courseid)
        {
            return _context.Courses.Find(Courseid);
        }

        public bool Save()
        {
            return _context.SaveChanges() == 1 ? true : false;
        }

        public bool Updatecourse(Course course)
        {
            _context.Courses.Update(course);
            return Save();
        }
    }
}
