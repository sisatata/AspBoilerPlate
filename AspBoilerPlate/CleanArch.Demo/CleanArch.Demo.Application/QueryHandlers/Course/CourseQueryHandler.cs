using AutoMapper;
using CleanArch.Demo.Application.Queries.Course;
using CleanArch.Demo.Application.Queries.Course.Model;
using CleanArch.Demo.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArch.Demo.Application.QueryHandlers.Course
{
    public class CourseQueryHandler : IRequestHandler<GetCoursesQuery, CourseDto>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _autoMapper;
        public CourseQueryHandler(ICourseRepository courseRepository, IMapper autoMapper)
        {
            _courseRepository = courseRepository;
            _autoMapper = autoMapper;
        }
        public async Task<CourseDto> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
        {
            var result = await _courseRepository.GetByIdAsync(request.Id);
            CourseDto courseDto = new CourseDto
            {
                Name = result.Name,
                Description = result.Description
            };
            return courseDto;
           

        }
    }
}
