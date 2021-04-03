using CleanArch.Demo.Domain.Commands;
using CleanArch.Demo.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArch.Demo.Application.Queries.CourseQuery.Model;
using Microsoft.AspNetCore.Authorization;

namespace CleanArch.Demo.Api.Controllers
{
    [Route("Api/[controller]")]

    [ApiController]
    [Authorize]
    public class CourseController : BaseController<CourseController>
    {
       
        #region ctor
        public CourseController()
        {  
           
        }
        #endregion
        #region methods
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Course course)
        {
          var data =   await _mediator.Send(new CreateCourseCommand { Description = course.Description, Name = course.Name });
          return Ok(data);
        }
        [HttpGet("{Id}")]

        public async Task<IActionResult> GetCourseById(Guid Id)
        {  
            var data = await _mediator.Send(new Application.Queries.CourseQuery.GetCoursesQuery { Id = Id });
            return Ok(data);
        }
        #endregion
    }
}
