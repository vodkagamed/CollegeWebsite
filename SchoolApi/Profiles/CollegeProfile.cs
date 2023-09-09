using AutoMapper;
using SchoolApi.Controllers.DTOs.Encoming;
using SchoolApi.Controllers.DTOs.Outcoming;
using SchoolWebsite.shared.Models;

namespace SchoolApi.Profiles
{
    public class CollegeProfile : Profile
    {
        public CollegeProfile()
        {
            CreateMap<College, CollegeCreateDTO>();
            CreateMap<CollegeCreateDTO, College>();
            CreateMap<College, CollegeResponse>()
                    .ForMember(dest => dest.StudentIds, opt => opt.MapFrom(src => src.Students.Select(s => s.Id).ToList()))
                    .ForMember(dest => dest.StudentNames, opt => opt.MapFrom(src => src.Students.Select(s => s.Name).ToList()))
                    .ForMember(dest => dest.TeacherIds, opt => opt.MapFrom(src => src.Teachers.Select(t => t.Id).ToList()))
                    .ForMember(dest => dest.TeacherNames, opt => opt.MapFrom(src => src.Teachers.Select(t => t.Name).ToList()))
                    .ForMember(dest => dest.CourseIds, opt => opt.MapFrom(src => src.Courses.Select(c => c.Id).ToList()))
                    .ForMember(dest => dest.CourseNames, opt => opt.MapFrom(src => src.Courses.Select(c => c.Name).ToList()));
        }
    }
}
