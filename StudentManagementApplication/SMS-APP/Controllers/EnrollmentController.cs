using Microsoft.AspNetCore.Mvc;
using SMS_APP.Models;
using SMS_APP.Repository.IRepository;

namespace SMS_APP.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;
        public EnrollmentController(IEnrollmentRepository enrollmentRepository,ICourseRepository courseRepository,IStudentRepository studentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
        }
        #region APIs
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userRole = SessionHandler.GetUserRole(HttpContext);
            var userEmail = SessionHandler.GetUserEmail(HttpContext);

            if (userRole == "Student")
            {
                var studentData = await _studentRepository.GetStudentByEmailAsync(userEmail, URL.StudentAPIPath);
                var student = studentData.FirstOrDefault();
                if (student != null)
                {
                    var studentEnrollments = await _enrollmentRepository.GetEnrollmentsByStudentId(student.Id);
                    return Json(new { data = studentEnrollments });
                }
                else
                {
                    return BadRequest("Student not found");
                }
            }
            else if (userRole == "Admin" || userRole == "Teacher")
            {
                var allData = await _enrollmentRepository.GetAllAsync(URL.EnrollmentAPIPath);
                return Json(new { data = allData });
            }
            else
            {
                return BadRequest("Unauthorized access");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var enrollment = await _enrollmentRepository.GetAsync(URL.EnrollmentAPIPath, id);
            if (enrollment == null)
                return Json(new { success = false, message = "Unable to delete the data" });

            await _enrollmentRepository.DeleteAsync(URL.EnrollmentAPIPath, id);
            return Json(new { success = true, message = "Data Deleted Successfully" });
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> SaveOrUpdate(int? studentId)
        {
            Enrollment enrollment = new Enrollment();
            enrollment.StudentId = studentId.GetValueOrDefault(); // Set the StudentId property
            if (studentId != null)
            {
                var student = await _studentRepository.GetAsync(URL.StudentAPIPath, studentId.Value);
                if (student != null)
                {
                    enrollment.Student = student;
                }
            }
            enrollment.CourseList = (List<Course>)await _courseRepository.GetAllAsync(URL.CourseAPIPath);
            return View(enrollment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveOrUpdate(Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                if (enrollment.Id != 0)
                {
                    await _enrollmentRepository.UpdateAsync(URL.EnrollmentAPIPath, enrollment);
                    return RedirectToAction(nameof(Index));
                }
                var studentEnrollments = await _enrollmentRepository.GetEnrollmentsByStudentId(enrollment.StudentId);
                if (studentEnrollments.Any())
                {
                    ModelState.AddModelError("", "The student is already enrolled.");
                    enrollment.Student = await _studentRepository.GetAsync(URL.StudentAPIPath, enrollment.StudentId);
                    enrollment.CourseList = (List<Course>)await _courseRepository.GetAllAsync(URL.CourseAPIPath);
                    return View(enrollment);
                }
                await _enrollmentRepository.CreateAsync(URL.EnrollmentAPIPath, enrollment);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                enrollment.CourseList = (List<Course>)await _courseRepository.GetAllAsync(URL.CourseAPIPath);
                return View(enrollment);
            }
        }
    }
}
