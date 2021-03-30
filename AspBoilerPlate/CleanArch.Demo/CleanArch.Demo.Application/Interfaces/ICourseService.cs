using CleanArch.Demo.Application.Commands.Model;
using CleanArch.Demo.Application.Queries.Course.Model;
using CleanArch.Demo.Application.ViewModels;
using CleanArch.Demo.Domain.Interfaces;
using CleanArch.Demo.Domain.Models;
using CleanArch.Demo.Infra.Core.Interfaces;
using CleanArch.Demo.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Demo.Application.Interfaces
{
    public interface ICourseService 
    {
        Task<CourseViewModel> GetCourses();

        Task CreateCourse(Course course);

        Task<CourseDto> GetCourseById(Guid Id);

    }
}
