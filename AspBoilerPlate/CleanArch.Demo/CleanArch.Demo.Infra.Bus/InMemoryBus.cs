using CleanArch.Demo.Application.Commands.Model;
using CleanArch.Demo.Domain.Core.Bus;
using CleanArch.Demo.Domain.Core.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Demo.Infra.Bus
{
   public class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task SendCommand<T>(T command)
        {
            return _mediator.Send(command);
        }

       
    }
}
