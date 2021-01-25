using CleanArch.Demo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Application.ViewModels
{
   public class CourseViewModel
    {
        public IEnumerable<Course> Courses { get; set; }
    }
}
