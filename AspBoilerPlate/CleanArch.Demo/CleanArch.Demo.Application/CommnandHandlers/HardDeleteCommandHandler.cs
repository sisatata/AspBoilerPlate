using CleanArch.Demo.Application.Commands;
using CleanArch.Demo.Application.Commands.Model;
using CleanArch.Demo.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArch.Demo.Application.CommnandHandlers
{
    public class HardDeleteCommandHandler : IRequestHandler<HardDeleteCourseCommand, CommonResponseDto>
    {
        private readonly ICourseRepository _courseRepository;
        public HardDeleteCommandHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;

        }
        public async Task<CommonResponseDto> Handle(HardDeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var response = new CommonResponseDto
            {
                Message = "Course can't be deleted",
                Status = false
            };
            try
            {
                var course = await _courseRepository.GetByIdAsync(request.Id);
                 await _courseRepository.DeleteAsync(course);
                response.Status = true;
                response.Message = "Course successfully Deleted";
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
