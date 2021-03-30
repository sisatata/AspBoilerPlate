using AutoMapper;
using CleanArch.Demo.Application.Commands.Model;
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
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _autoMapper;
        public CourseCommandHandler(ICourseRepository courseRepository, IMapper autoMapper)
        {
            _courseRepository = courseRepository;
            _autoMapper = autoMapper;
        }

        public async Task<bool> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            
            try
            {
                var result =  await _courseRepository.AddAsync(_autoMapper.Map<Course>(request));

                return true; ;

            }
            catch(Exception ex)
            {
                throw ex;
            }


        }
    }
}
