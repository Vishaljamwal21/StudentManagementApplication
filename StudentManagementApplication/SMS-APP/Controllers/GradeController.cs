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
            //var grades = await _gradeRepository.GetAllAsync(URL.GradeAPIPath);
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
            Grade grade = new Grade
            {
                Enrollment = enrollment
            };

            return View(grade);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveOrUpdate(Grade grade)
        {
            if (ModelState.IsValid)
            {
                var enrollment = await _enrollmentRepository.GetAsync(URL.EnrollmentAPIPath, grade.EnrollmentId);
                if (enrollment.Course == null)
                {
                    ModelState.AddModelError("", "Enrollment does not have a valid associated Course.");
                    return View(grade);
                }
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
