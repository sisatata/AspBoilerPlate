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
   public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommandV1, CommonResponseDto>
    {
        private readonly ICourseRepository _courseRepository;
        public DeleteCourseCommandHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<CommonResponseDto> Handle(DeleteCourseCommandV1 request, CancellationToken cancellationToken)
        {
            var response = new CommonResponseDto
            {
                Message = "Course can't be deleted",
                Status = false
            };
            try
            {
                var course = await _courseRepository.GetByIdAsync(request.Id);
                course.IsDeleted = true;
                await _courseRepository.UpdateAsync(course);
                response.Status = true;
                response.Message = "Course successfully Deleted";
                return response;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
