using AutoMapper;
using CleanArch.Demo.Application.Interfaces;
using CleanArch.Demo.Application.ViewModels;
using CleanArch.Demo.Domain.Commands;
using CleanArch.Demo.Domain.Core.Bus;
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
        private readonly IAsyncCourseRepository _courseRepository;
        private readonly IMediatorHandler _bus;
        private readonly IMapper _autoMapper;
          public CourseService(IAsyncCourseRepository courseRepository, IMediatorHandler bus, IMapper autoMapper)
        {
            _courseRepository = courseRepository;
            _bus = bus;
            _autoMapper = autoMapper;
        }  

        public async  Task<CourseViewModel> GetCourses()
        {

            return null;
       
    }

        public async Task CreateCourse(Course course)
        {
            //await _courseRepository.CreateCourse(course);
          /*  var createCourseCommand = new CreateCourseCommand(
                  course.Name,
                  course.Description
                );*/

            await _bus.SendCommand(_autoMapper.Map<CreateCourseCommand>(course));
            
        }

        public  async Task<Course> GetCourseById(Guid Id)
        {
            return await _courseRepository.GetCourseById(Id);
        }

        


        

    }
}
