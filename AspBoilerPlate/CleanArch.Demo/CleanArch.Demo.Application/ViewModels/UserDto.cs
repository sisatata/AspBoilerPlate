using AutoMapper;
using CleanArch.Demo.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Application.ViewModels
{
    [AutoMap(typeof(ApplicationUser))]
    public class UserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
