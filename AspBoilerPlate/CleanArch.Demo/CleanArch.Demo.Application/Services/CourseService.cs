using AutoMapper;
using CleanArch.Demo.Application.Commands.Model;
using CleanArch.Demo.Application.Interfaces;
using CleanArch.Demo.Application.Queries.CourseQuery.Model;
using CleanArch.Demo.Application.ViewModels;
using CleanArch.Demo.Domain.Commands;

using CleanArch.Demo.Domain.Interfaces;
using CleanArch.Demo.Domain.Models;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Demo.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
  
        private readonly IMapper _autoMapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IMediator _mediator;
        public Course list;
        public CourseService(ICourseRepository courseRepository,  IMapper autoMapper, IMemoryCache memoryCache, IMediator mediator)
        {
            _courseRepository = courseRepository;
         
            _autoMapper = autoMapper;
            _memoryCache = memoryCache;
            _mediator = mediator;
        }

        public async Task<CourseViewModel> GetCourses()
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

            //  await _bus.SendCommand(_autoMapper.Map<CreateCourseCommand>(course));
            await _mediator.Send(new CreateCourseCommand { Description = course.Description, Name=course.Name });

          
        }

        public async Task<CourseDto> GetCourseById(Guid Id)
        {
            /*
               if (!_memoryCache.TryGetValue(Id, out Course course))
               {
                    list = await _courseRepository.GetCourseById(Id);
                   var cahceExpirationOption = new MemoryCacheEntryOptions
                   {
                       AbsoluteExpiration = DateTime.Now.AddHours(6)

                   };
                   _memoryCache.Set(Id, list, cahceExpirationOption);
               }

               return _memoryCache.Get<Course>(Id);*/

            return await _mediator.Send(new Queries.CourseQuery.GetCoursesQuery { Id = Id });

        }

        
    }
}
