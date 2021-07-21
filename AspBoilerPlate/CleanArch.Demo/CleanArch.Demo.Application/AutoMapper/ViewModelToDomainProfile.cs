using AutoMapper;
using CleanArch.Demo.Application.ViewModels;
using CleanArch.Demo.Domain.Commands;
using CleanArch.Demo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Application.AutoMapper
{
   public class ViewModelToDomainProfile :Profile
    {
        public ViewModelToDomainProfile()
        {
            CreateMap<Course, CreateCourseCommandV1>()
                .ConstructUsing(c => new CreateCourseCommandV1());

            CreateMap<Course, UpdateCourseDto>()
               .ConstructUsing(c => new UpdateCourseDto());
        }

    }
}
