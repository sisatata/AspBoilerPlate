using CleanArch.Demo.Application.Interfaces;
using CleanArch.Demo.Application.Queries.CourseQuery.Model;
using CleanArch.Demo.Domain.Models;
using CleanArch.Demo.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Application.Queries.CourseQuery
{
   public class GetAllCourseQuery:  IRequest<PagedResponse<List<CourseDto>>>
    {
        public string Path { get; set; }
         public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public IUriService UriService { get; set; }
    }
}
