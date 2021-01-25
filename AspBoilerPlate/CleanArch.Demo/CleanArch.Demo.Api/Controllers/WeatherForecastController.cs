﻿using CleanArch.Demo.Application.Interfaces;
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

        private readonly ICourseService _courseService;
        private readonly IAsyncCourseRepository<Course, int> _courseRepository;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ICourseService courseService  , IAsyncCourseRepository<Course, int> courseRepository)

        {
            _logger = logger;
            _courseService = courseService;
            _courseRepository = courseRepository;
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

        [HttpPost]

        public async Task<IActionResult> CreateCourse([FromBody] Course command)
        {
            await _courseRepository.AddAsync(command);
            return Ok();
        }

        [HttpGet("{Id}")]

        public async Task<IActionResult> GetCourseById(int Id)
        {
            var data = await _courseService.GetCourseById(Id);
            return Ok(data);
        }


    }
}
