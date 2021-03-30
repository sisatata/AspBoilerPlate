﻿using AutoMapper;
using CleanArch.Demo.Application.Queries.Course.Model;
using CleanArch.Demo.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Application.Queries.Course
{
   
    public class GetCoursesQuery : IRequest<CourseDto>
    {
       public Guid Id { get; set; }
    }
}