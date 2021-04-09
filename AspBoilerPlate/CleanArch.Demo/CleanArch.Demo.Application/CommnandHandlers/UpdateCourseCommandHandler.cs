using AutoMapper;
using CleanArch.Demo.Application.Commands;
using CleanArch.Demo.Application.Commands.Model;
using CleanArch.Demo.Domain.Interfaces;
using CleanArch.Demo.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArch.Demo.Application.CommnandHandlers
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, CommonResponseDto>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _autoMapper;
        public UpdateCourseCommandHandler(ICourseRepository courseRepository, IMapper autoMapper)
        {
            _courseRepository = courseRepository;
            _autoMapper = autoMapper;
        }
        public async Task<CommonResponseDto> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var response = new CommonResponseDto
            {
                Message = "Course can't be Updated",
                Status = false
            };
            try
            {
                var course = await _courseRepository.GetByIdAsync(request.Id);
                // need to use auto mapper
                course.Name = request.Name;
                course.Description = request.Description;
                await _courseRepository.UpdateAsync(course);
                response.Status = true;
                response.Message = "Course successfully Updated";
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
