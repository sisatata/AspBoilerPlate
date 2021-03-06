using AutoMapper;
using CleanArch.Demo.Application.Commands;
using CleanArch.Demo.Domain.Commands;
using CleanArch.Demo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Application.AutoMapper
{
   public class DomainToViewModelProfile : Profile
    {
        public DomainToViewModelProfile()
        {
            CreateMap<CreateCourseCommand, Course>();
            CreateMap<CreateRegisterCommand, RegisterVM>();
            //  CreateMap<CreateCourseCommand, Course>();
        }
    }
}
