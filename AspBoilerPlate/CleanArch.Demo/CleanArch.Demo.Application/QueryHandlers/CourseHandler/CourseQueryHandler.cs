using AutoMapper;
using CleanArch.Demo.Application.Queries.CourseQuery;
using CleanArch.Demo.Application.Queries.CourseQuery.Model;
using CleanArch.Demo.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CleanArch.Demo.Domain.Models;

namespace CleanArch.Demo.Application.QueryHandlers.CourseHandler
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
            return _autoMapper.Map<CourseDto>(result);

        }
    }
}
