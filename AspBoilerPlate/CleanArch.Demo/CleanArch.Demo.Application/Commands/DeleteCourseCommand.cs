using CleanArch.Demo.Application.Commands.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Application.Commands
{
   public class DeleteCourseCommandV1 : IRequest<CommonResponseDto>
    {
        public Guid Id { get; set; }
    }
}
