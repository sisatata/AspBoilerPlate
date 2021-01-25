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
        private readonly IAsyncCourseRepository<Course, int> _courseRepository;

        public CourseCommandHandler(IAsyncCourseRepository<Course, int> courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<bool> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var course = new Course()
            {
                Name = request.Name,
                Description = request.Description
            };


            await _courseRepository.AddAsync(course);
            return await Task.FromResult(true);


        }
    }
}
