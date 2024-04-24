using AutoMapper;
using StudentManagementSystum.Models;
using StudentManagementSystum.Models.DTO;
using System.Diagnostics;

namespace StudentManagementSystum.DTOMappings
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentDTO>().ReverseMap();
            CreateMap<CourseDTO, Course>().ReverseMap();
            CreateMap<EnrollmentDTO, Enrollment>().ReverseMap();
            CreateMap<GradeDTO, Grade>().ReverseMap();
        }
    }
}
