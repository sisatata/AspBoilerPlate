using AutoMapper;
using CleanArch.Demo.Application.Commands.Model;
using CleanArch.Demo.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Application.Commands
{
    [AutoMap(typeof(Course))]
    public class UpdateCourseCommand : IRequest<CommonResponseDto>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid Id { get; set; }
    }
}
