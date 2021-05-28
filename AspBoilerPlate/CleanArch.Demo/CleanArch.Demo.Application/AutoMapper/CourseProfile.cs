using AutoMapper;
using CleanArch.Demo.Domain.Commands;
using CleanArch.Demo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Application.AutoMapper
{
   internal class CourseProfile : Profile
    {
        public CourseProfile()
        {
            CreateMap<CreateCourseCommand, Course>().ReverseMap();
        }
    }
}
