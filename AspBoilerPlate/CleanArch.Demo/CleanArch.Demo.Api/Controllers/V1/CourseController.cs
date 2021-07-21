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
using CleanArch.Demo.Shared.Constants.Security;
using AutoMapper;
using CleanArch.Demo.Application.Commands;
using CleanArch.Demo.Application.ViewModels;
using CleanArch.Demo.Shared;
using CleanArch.Demo.Application.Interfaces;

namespace CleanArch.Demo.Api.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
   
    public class CourseController : BaseController<CourseController>
    {
        private readonly IMapper _autoMapper;
        private readonly IUriService _uriService;
        #region ctor
        public CourseController(IMapper autoMapper, IUriService uriService)
        {
            _autoMapper = autoMapper;
            _uriService = uriService;


        }
        #endregion
        #region methods
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCourseCommand course)
        {
          var data =   await _mediator.Send(course);
          return Ok(data);
        }
        [HttpGet("{Id}")]

        public async Task<IActionResult> GetCourseById(Guid Id)
        {  
            var data = await _mediator.Send(new Application.Queries.CourseQuery.GetCoursesQuery { Id = Id });
            return Ok(data);
        }
        [HttpGet("Get-All")]
       
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var data = await _mediator.Send(new Application.Queries.CourseQuery.GetAllCourseQuery{ Path = route, PageNumber= validFilter.PageNumber, PageSize= validFilter.PageSize, UriService = _uriService});
            return Ok(data);
        }
        [HttpPost("delete-course")]
        public async Task<IActionResult> DeleteCourseById(Guid Id)
        {
            var result = await _mediator.Send(new Application.Commands.DeleteCourseCommand { Id = Id });
            return Ok(result);
        }
        [HttpPost("hard-delete-course")]
        public async Task<IActionResult> HardDeleteCourseById(Guid Id)
        {
            var result = await _mediator.Send(new Application.Commands.HardDeleteCourseCommand { Id = Id });
            return Ok(result);
        }

        [HttpPut("update-course")]
        public async Task<IActionResult> UpdateCourseById([FromBody] UpdateCourseDto  model)
        {
            var result = await _mediator.Send(new Application.Commands.UpdateCourseCommand{ Name = model.Name, Description = model.Description , Id=model.Id});
            return Ok(result);
        }
        #endregion
    }
}
