using CleanArch.Demo.Application.Interfaces;
using CleanArch.Demo.Application.ViewModels;
using CleanArch.Demo.Domain.Interfaces;
using CleanArch.Demo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Demo.Application.Services
{
  public  class CourseService: ICourseService
    {
        private readonly IAsyncCourseRepository<Domain.Models.Course,int> _courseRepository;
          public CourseService(IAsyncCourseRepository<Domain.Models.Course, int> courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async  Task<CourseViewModel> GetCourses()
        {

            return null;
       
    }

        public async Task CreateCourse(Course course)
        {
            await _courseRepository.CreateCourse(course);
        }

        public  async Task<Course> GetCourseById(int Id)
        {
            return await _courseRepository.GetCourseById(Id);
        }
        

    }
}
