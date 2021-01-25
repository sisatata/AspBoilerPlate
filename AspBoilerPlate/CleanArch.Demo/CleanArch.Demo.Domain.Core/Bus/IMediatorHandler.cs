using CleanArch.Demo.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch.Demo.Domain.Core.Bus
{
     public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
    }
}
