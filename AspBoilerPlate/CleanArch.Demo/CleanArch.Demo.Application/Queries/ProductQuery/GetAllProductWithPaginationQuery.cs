using CleanArch.Demo.Application.Queries.ProductQuery.Model;
using CleanArch.Demo.Infra.Core.Specifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Application.Queries.ProductQuery
{
   public class GetAllProductWithPaginationQuery : IRequest<IList<ProductDto>>
    {
        public ProductSpecParams ProductParams { get; set; }
    }
}
