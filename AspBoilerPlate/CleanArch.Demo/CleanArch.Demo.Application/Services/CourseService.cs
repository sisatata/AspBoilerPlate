using AutoMapper;
using CleanArch.Demo.Application.Interfaces;
using CleanArch.Demo.Application.ViewModels;
using CleanArch.Demo.Domain.Commands;
using CleanArch.Demo.Domain.Core.Bus;
using CleanArch.Demo.Domain.Interfaces;
using CleanArch.Demo.Domain.Models;
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
        private readonly IMediatorHandler _bus;
        private readonly IMapper _autoMapper;
        private readonly IMemoryCache _memoryCache;
        public Course list;
        public CourseService(ICourseRepository courseRepository, IMediatorHandler bus, IMapper autoMapper, IMemoryCache memoryCache)
        {
            _courseRepository = courseRepository;
            _bus = bus;
            _autoMapper = autoMapper;
            _memoryCache = memoryCache;
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

            await _bus.SendCommand(_autoMapper.Map<CreateCourseCommand>(course));

        }

        public async Task<Course> GetCourseById(Guid Id)
        {
         
            if (!_memoryCache.TryGetValue(Id, out Course course))
            {
                 list = await _courseRepository.GetCourseById(Id);
                var cahceExpirationOption = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddHours(6)

                };
                _memoryCache.Set(Id, list, cahceExpirationOption);
            }

            return _memoryCache.Get<Course>(Id);
        }






    }
}
