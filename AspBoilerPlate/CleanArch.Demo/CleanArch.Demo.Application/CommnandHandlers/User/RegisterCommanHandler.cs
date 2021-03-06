using AutoMapper;
using CleanArch.Demo.Application.Commands;
using CleanArch.Demo.Application.Interfaces;
using CleanArch.Demo.Domain.Interfaces;
using CleanArch.Demo.Domain.Models;
using CleanArch.Demo.Infra.Data.Context;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArch.Demo.Application.CommnandHandlers.User
{
    public class RegisterCommanHandler : IRequestHandler<CreateRegisterCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _autoMapper;

        public RegisterCommanHandler(IUserRepository userRepository, IMapper autoMapper)
        {
            _userRepository = userRepository;
            _autoMapper = autoMapper;

        }
        public async Task<bool> Handle(CreateRegisterCommand request, CancellationToken cancellationToken)
        {
            await _userRepository.RegisterUser(_autoMapper.Map<RegisterVM>(request));
            return await Task.FromResult(true);
        }
    }
}
