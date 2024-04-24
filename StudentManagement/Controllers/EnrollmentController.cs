using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystum.Models;
using StudentManagementSystum.Models.DTO;
using StudentManagementSystum.Repository.IRepository;
using System.Collections.Generic;

namespace StudentManagementSystum.Controllers
{
    [Route("api/Enrollment")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IMapper _mapper;

        public EnrollmentController(IEnrollmentRepository enrollmentRepository, IMapper mapper)
        {
            _enrollmentRepository = enrollmentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetEnrollments()
        {
            var enrollments = _enrollmentRepository.GetEnrollments();
            var enrollmentsDto = _mapper.Map<IEnumerable<EnrollmentDTO>>(enrollments);
            return Ok(enrollmentsDto);
        }

        [HttpGet("{enrollmentid:int}",Name ="GetEnrollment")]
        public IActionResult GetEnrollment(int id)
        {
            var enrollment = _enrollmentRepository.GetEnrollment(id);
            if (enrollment == null)
            {
                return NotFound();
            }
            var enrollmentDto = _mapper.Map<EnrollmentDTO>(enrollment);
            return Ok(enrollmentDto);
        }

        [HttpPost]
        public IActionResult CreateEnrollment(EnrollmentDTO enrollmentDto)
        {
            try
            {
                if (enrollmentDto == null)
                {
                    return BadRequest();
                }

                var enrollment = _mapper.Map<Enrollment>(enrollmentDto);
                if (!_enrollmentRepository.CreateEnrollment(enrollment))
                {
                    return StatusCode(500, "Internal server error while creating the enrollment.");
                }

                return CreatedAtAction(nameof(GetEnrollment), new { id = enrollment.Id }, enrollment);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                Console.WriteLine($"An error occurred: {ex}");

                // Return a generic error message
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        [HttpPut("{id}")]
        public IActionResult UpdateEnrollment(int id, EnrollmentDTO enrollmentDto)
        {
            if (id != enrollmentDto.Id)
            {
                return BadRequest("Enrollment ID mismatch");
            }

            var enrollment = _mapper.Map<Enrollment>(enrollmentDto);
            if (!_enrollmentRepository.UpdateEnrollment(enrollment))
            {
                return StatusCode(500, "Internal server error while updating the enrollment.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEnrollment(int id)
        {
            var enrollment = _enrollmentRepository.GetEnrollment(id);
            if (enrollment == null)
            {
                return NotFound();
            }

            if (!_enrollmentRepository.DeleteEnrollment(enrollment))
            {
                return StatusCode(500, "Internal server error while deleting the enrollment.");
            }

            return NoContent();
        }
    }
}
