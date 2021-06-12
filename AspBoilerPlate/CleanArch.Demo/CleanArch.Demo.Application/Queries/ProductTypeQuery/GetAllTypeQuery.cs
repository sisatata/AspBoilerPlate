using CleanArch.Demo.Application.Queries.ProductTypeQuery.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Application.Queries.ProductTypeQuery
{
   public class GetAllTypeQuery : IRequest<IList<TypeDto>>
    {
    }
}
