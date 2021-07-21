using AutoMapper;
using CleanArch.Demo.Application.Commands.Model;
using CleanArch.Demo.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Domain.Commands
{
    [AutoMap(typeof(Course))]
    public class CreateCourseCommandV1 : IRequest <CommonResponseDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }


    }
  
}
