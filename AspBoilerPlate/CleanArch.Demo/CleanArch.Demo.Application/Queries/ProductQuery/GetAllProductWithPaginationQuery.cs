using CleanArch.Demo.Application.Queries.ProductQuery.Model;
using CleanArch.Demo.Infra.Core.Specifications;
using CleanArch.Demo.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Application.Queries.ProductQuery
{
   public class GetAllProductWithPaginationQueryV1 : IRequest<Pagination<ProductToReturnDto>>
    {
        public ProductSpecParams ProductParams { get; set; }
    }
}
