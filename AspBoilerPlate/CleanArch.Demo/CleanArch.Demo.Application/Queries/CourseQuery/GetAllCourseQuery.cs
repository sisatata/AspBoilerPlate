using CleanArch.Demo.Application.Queries.CourseQuery.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Application.Queries.CourseQuery
{
   public class GetAllCourseQuery:  IRequest<List<CourseDto>>
    {
    }
}
