using AutoMapper;
using CleanArch.Demo.Application.Queries.ProductQuery;
using CleanArch.Demo.Application.Queries.ProductQuery.Model;
using CleanArch.Demo.Application.Queries.ProductTypeQuery.Model;
using CleanArch.Demo.Domain;
using CleanArch.Demo.Domain.Models;
using CleanArch.Demo.Infra.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArch.Demo.Application.QueryHandlers.ProductTypeHandler
{
    public class GetAllProductWithPaginationQueryHandler : IRequestHandler<GetAllProductWithPaginationQuery, IList<ProductDto>>
    {
        private readonly IAsyncRepository<Product, Guid> _productRepository;
        private readonly IMapper _autoMapper;
        private readonly IDistributedCache _distributedCache;
        public GetAllProductWithPaginationQueryHandler(IAsyncRepository<Product, Guid> productRepository, IMapper autoMapper)
        {
            _productRepository = productRepository;
            _autoMapper = autoMapper;
        }
        public Task<IList<ProductDto>> Handle(GetAllProductWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(request.ProductParams);
            return null;
        }
    }
}
