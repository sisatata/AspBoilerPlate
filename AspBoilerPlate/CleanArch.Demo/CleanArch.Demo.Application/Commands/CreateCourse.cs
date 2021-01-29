using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Domain.Commands
{
  public  class CreateCourse  : IRequest<CreateCourse>
    {

        public string Name { get; set; }
        public string Description { get; set; }
    }
}
