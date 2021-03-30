using AutoMapper;
using CleanArch.Demo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Application.Queries.CourseQuery.Model
{
  [AutoMap(typeof (Course))]
   public class CourseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
