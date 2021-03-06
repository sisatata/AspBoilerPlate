using CleanArch.Demo.Application.Commands;
using CleanArch.Demo.Domain.Models;
using CleanArch.Demo.Infra.Data.Context;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArch.Demo.Api.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator, SignInManager<ApplicationUser> signInManager)
        {
            _mediator = mediator;
            _signInManager = signInManager;

        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {

            RegisterVM registerViewModel = new RegisterVM();
           
            return View(registerViewModel);
        }
        [HttpPost("Register")]
        
        public async Task<IActionResult> Register([FromBody] CreateRegisterCommand command)
        {

            var data = await _mediator.Send(command);
            return Ok(data);
        }

        [HttpPost("Logout")]

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
