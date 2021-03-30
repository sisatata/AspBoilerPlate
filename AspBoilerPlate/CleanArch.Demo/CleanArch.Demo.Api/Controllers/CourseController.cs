using CleanArch.Demo.Application.Interfaces;
using CleanArch.Demo.Domain.Commands;
using CleanArch.Demo.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArch.Demo.Api.Controllers
{
    [Route("Api/[controller]")]

    [ApiController]
    public class CourseController : ControllerBase
    {
        #region properties
        private readonly ICourseService _courseService;
        private readonly IMediator _mediator;
        #endregion
        #region ctor
        public CourseController(ICourseService courseService, IMediator mediator)
        {
            _courseService = courseService;
            _mediator = mediator;
        }
        #endregion
        #region methods
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Course course)
        {
           // var now = new CreateCourseCommand(course.Name, course.Description);
           await _courseService.CreateCourse(course);
            //await _mediator.Send(now);

            return Ok(true);
        }
        #endregion
    }
}
