using Microsoft.AspNetCore.Mvc;
using SMS_APP.Models;
using SMS_APP.Repository.IRepository;
using System.Threading.Tasks;

namespace SMS_APP.Controllers
{
    public class GradeController : Controller
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IStudentRepository _studentRepository;
        public GradeController(IGradeRepository gradeRepository, IEnrollmentRepository enrollmentRepository,IStudentRepository studentRepository)
        {
            _gradeRepository = gradeRepository;
            _enrollmentRepository = enrollmentRepository;
            _studentRepository = studentRepository;
        }

        #region APIs

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var grades = await _gradeRepository.GetAllAsync(URL.GradeAPIPath);
            return Json(new { data = grades });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var grade = await _gradeRepository.GetAsync(URL.GradeAPIPath, id);
            if (grade == null)
                return Json(new { success = false, message = "Unable to delete the data" });

            await _gradeRepository.DeleteAsync(URL.GradeAPIPath, id);
            return Json(new { success = true, message = "Data Deleted Successfully" });
        }

        #endregion

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> SaveOrUpdate(int? enrollmentId)
        {
            if (enrollmentId == null)
            {
                return BadRequest("Enrollment ID is required.");
            }
            var enrollment = await _enrollmentRepository.GetAsync(URL.EnrollmentAPIPath, enrollmentId.Value);
            if (enrollment == null)
            {
                return NotFound("Enrollment not found.");
            }
            string studentName = enrollment.Student?.Name;
            string courseTitle = enrollment.Course?.Title;

            Grade grade = new Grade
            {
                Enrollment = enrollment
            };
            ViewData["StudentName"] = studentName;
            ViewData["CourseTitle"] = courseTitle;
            return View(grade);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveOrUpdate(Grade grade)
        {
            if (ModelState.IsValid)
            {
                var existingGrade = await _gradeRepository.GetByEnrollmentIdAsync(URL.GradeAPIPath, grade.EnrollmentId);
                if (existingGrade != null)
                {
                    var enroll = await _enrollmentRepository.GetAsync(URL.EnrollmentAPIPath, grade.EnrollmentId);
                    string studentName = enroll.Student?.Name;
                    string courseTitle = enroll.Course?.Title;
                    ViewData["StudentName"] = studentName;
                    ViewData["CourseTitle"] = courseTitle;

                    ModelState.AddModelError("", "A grade for this enrollment already exists.");
                    return View(grade);
                }
                var enrollment = await _enrollmentRepository.GetAsync(URL.EnrollmentAPIPath, grade.EnrollmentId);
                if (enrollment.Course == null)
                {
                    ModelState.AddModelError("", "Enrollment does not have a valid associated Course.");
                    return View(grade);
                }
                grade.Enrollment.CourseId = enrollment.Course.Id;

                // If no existing grade is found, proceed with saving or updating the grade
                if (grade.Id == 0)
                {
                    await _gradeRepository.CreateAsync(URL.GradeAPIPath, grade);
                }
                else
                {
                    await _gradeRepository.UpdateAsync(URL.GradeAPIPath, grade);
                }
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(grade);
            }
        }
    }
}
