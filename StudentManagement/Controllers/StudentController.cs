using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystum.Models;
using StudentManagementSystum.Models.DTO;
using StudentManagementSystum.Repository.IRepository;

namespace StudentManagementSystum.Controllers
{
    [Route("api/Student")]
    [ApiController]
    //[Authorize]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        public StudentController(IStudentRepository studentRepository,IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetStudent()
        {
            var studentDTO = _studentRepository.GetStudent().ToList().Select(_mapper.Map<Student, StudentDTO>);
            return Ok(studentDTO);//200
        }
        [HttpGet("{studentId:int}", Name = "GetStudent")]
        public IActionResult GetStudent(int studentid)
        {
            var student = _studentRepository.GetStudent(studentid);
            if (student == null) return NotFound();//404
            var studentDTO = _mapper.Map<StudentDTO>(student);
            return Ok(studentDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] StudentDTO studentDTO)
        {
            if (studentDTO == null) return BadRequest(ModelState); //400
            if (!await _studentRepository.IsUniqueEmail(studentDTO.Email))
            {
                ModelState.AddModelError("", "Email already exists.");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if (!ModelState.IsValid) return BadRequest(); //400 Error

            var student = _mapper.Map<StudentDTO, Student>(studentDTO);

            if (!_studentRepository.Createstudent(student))
            {
                ModelState.AddModelError("", $"Something went wrong while saving data: {student.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtRoute("GetStudent", new { studentid = student.Id }, student);
        }
        [HttpPut]
        public IActionResult UpdateStudent([FromBody] StudentDTO studentDTO)
        {
            if (studentDTO == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            var student = _mapper.Map<Student>(studentDTO);
            if (!_studentRepository.Updatestudent(student))
            {
                ModelState.AddModelError("", $"Something Went Wrong while Updating the data");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }
        [HttpDelete("{studentid:int}")]
        public IActionResult DeleteStudent(int studentid)
        {
            if (!_studentRepository.StudentExists(studentid)) return BadRequest();
            var student = _studentRepository.GetStudent(studentid);
            if (student == null) return BadRequest();
            if (!_studentRepository.Deletestudent(student))
            {
                ModelState.AddModelError("", "Something Went Wrong while deleting data");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }

    }
}
