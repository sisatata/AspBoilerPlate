using AutoMapper;
using CleanArch.Demo.Application.ViewModels;
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
            CreateMap<UpdateCourseDto, Course>();
            CreateMap<CreateCourseCommandV1, Course>();
           
            //  CreateMap<CreateCourseCommand, Course>();
        }
    }
}
