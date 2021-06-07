using CleanArch.Demo.Application.Queries.CourseQuery.Model;
using CleanArch.Demo.Application.Queries.ProductQuery.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Application.Queries.ProductQuery
{
  public  class GetAllBrandQuery : IRequest<List<BrandDto>>
    {


    }
}
