using AutoMapper;
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
            CreateMap<Course, CreateCourseCommand>()
                .ConstructUsing(c => new CreateCourseCommand(c.Name, c.Description));
        }

    }
}
