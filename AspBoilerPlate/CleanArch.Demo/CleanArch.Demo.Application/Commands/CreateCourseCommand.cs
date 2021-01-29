using AutoMapper;
using CleanArch.Demo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Domain.Commands
{
    [AutoMap(typeof(Course))]
    public class CreateCourseCommand : CourseCommand
    {
        public CreateCourseCommand(string name, string description)
        {
            Name = name;
            Description = description;
        }


    }
  
}
