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
    public class CourseCommandHandler : IRequestHandler<CreateCourseCommand, CommonResponseDto>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _autoMapper;
        public CourseCommandHandler(ICourseRepository courseRepository, IMapper autoMapper)
        {
            _courseRepository = courseRepository;
            _autoMapper = autoMapper;
        }

        public async Task<CommonResponseDto> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {
            var commonResponseDto = new CommonResponseDto
            {
                Status = false,
                Message = "Failed"
            };
            try
            {
                //  Course course = new Course();
                var course = _autoMapper.Map<Course>(request);
                var result = await _courseRepository.AddAsync(course);
                commonResponseDto.ApplicationId = result.Id;
                commonResponseDto.Status = true;
                commonResponseDto.Message = "Entity Successfully Created";
                return commonResponseDto;
            }
            catch(Exception ex)
            {
                throw ex;
            }


        }
    }
}
