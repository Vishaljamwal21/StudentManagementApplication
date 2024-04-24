using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystum.Models.DTO;
using StudentManagementSystum.Models;
using StudentManagementSystum.Repository;
using StudentManagementSystum.Repository.IRepository;

namespace StudentManagementSystum.Controllers
{
    [Route("api/Course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        public CourseController(ICourseRepository courseRepository, IMapper mapper)
        {
            _courseRepository=courseRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult GetCourse()
        {
            var courseDTO = _courseRepository.GetCourse().ToList().Select(_mapper.Map<Course,CourseDTO>);
            return Ok(courseDTO);//200
        }
        [HttpGet("{courseid:int}", Name = "GetCourse")]
        public IActionResult GetCourse(int courseid)
        {
            var course = _courseRepository.GetCourse(courseid);
            if (course == null) return NotFound();//404
            var courseDTO = _mapper.Map<CourseDTO>(course);
            return Ok(courseDTO);
        }
        [HttpPost]
        public IActionResult CreateCourse([FromBody] CourseDTO courseDTO)
        {
            if (courseDTO == null) return BadRequest(ModelState);//400
            if (_courseRepository.CourseExists(courseDTO.Title))
            {
                ModelState.AddModelError("", "Course in DB !!!!");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            if (!ModelState.IsValid) return BadRequest();//400 Error
            var course = _mapper.Map<CourseDTO, Course>(courseDTO);

            if (!_courseRepository.Createcourse(course))
            {
                ModelState.AddModelError("", $"Something went wrong while saving data:{course.Title}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            //return Ok();//200
            return CreatedAtRoute("GetCourse",
                new { courseid = course.Id }, course);
        }
        [HttpPut]
        public IActionResult UpdateCourse([FromBody] CourseDTO courseDTO)
        {
            if (courseDTO == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            var course = _mapper.Map<Course>(courseDTO);
            if (!_courseRepository.Updatecourse(course))
            {
                ModelState.AddModelError("", $"Something Went Wrong while Updating the data");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }
        [HttpDelete("{courseid:int}")]
        public IActionResult DeleteCourse(int courseid)
        {
            if (!_courseRepository.CourseExists(courseid)) return BadRequest();
            var course = _courseRepository.GetCourse(courseid);
            if (course == null) return BadRequest();
            if (!_courseRepository.Deletecourse(course))
            {
                ModelState.AddModelError("", "Something Went Wrong while deleting data");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }
    }
}
