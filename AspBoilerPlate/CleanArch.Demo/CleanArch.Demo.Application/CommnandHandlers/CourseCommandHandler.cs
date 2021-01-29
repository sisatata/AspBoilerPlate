using AutoMapper;
using CleanArch.Demo.Domain.Commands;
using CleanArch.Demo.Domain.Interfaces;
using CleanArch.Demo.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArch.Demo.Domain.CommnandHandlers
{
    public class CourseCommandHandler : IRequestHandler<CreateCourseCommand, bool>
    {
        private readonly IAsyncCourseRepository _courseRepository;
        private readonly IMapper _autoMapper;
        public CourseCommandHandler(IAsyncCourseRepository courseRepository, IMapper autoMapper)
        {
            _courseRepository = courseRepository;
            _autoMapper = autoMapper;
        }

        public async Task<bool> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            /*var course = new Course()
            {
                Name = request.Name,
                Description = request.Description
            };*/


            await _courseRepository.AddAsync(_autoMapper.Map<Course>(request));
            return await Task.FromResult(true);


        }
    }
}
