using CleanArch.Demo.Application.Interfaces;
using CleanArch.Demo.Application.ViewModels;
using CleanArch.Demo.Domain.Interfaces;
using CleanArch.Demo.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArch.Demo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ICourseRepository _courseRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger , ICourseRepository courseRepository, IEmailSender emailSender)

        {
            _logger = logger;
         
            _courseRepository = courseRepository;
            _emailSender = emailSender;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet("send-mail")]
        public void SendMail()
        {
            try
            {
                var rng = new Random();
                var message = new Message(new string[] { "aansadiqul@gmail.com" }, "Test email", "This is the content from our email.");
               // _emailSender.SendEmail(message);
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }
    }
}
