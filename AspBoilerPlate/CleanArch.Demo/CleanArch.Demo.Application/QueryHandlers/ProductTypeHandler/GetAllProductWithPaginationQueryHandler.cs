using AutoMapper;
using CleanArch.Demo.Application.Queries.ProductQuery;
using CleanArch.Demo.Application.Queries.ProductQuery.Model;
using CleanArch.Demo.Application.Queries.ProductTypeQuery.Model;
using CleanArch.Demo.Domain;
using CleanArch.Demo.Domain.Models;
using CleanArch.Demo.Infra.Core.Interfaces;
using CleanArch.Demo.Shared;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArch.Demo.Application.QueryHandlers.ProductTypeHandler
{
    public class GetAllProductWithPaginationQueryHandler : IRequestHandler<GetAllProductWithPaginationQueryV1, Pagination<ProductToReturnDto>>
    {
        private readonly IAsyncRepository<Product, Guid> _productRepository;
        private readonly IMapper _autoMapper;
        private readonly IDistributedCache _distributedCache;
        public GetAllProductWithPaginationQueryHandler(IAsyncRepository<Product, Guid> productRepository, IMapper autoMapper)
        {
            _productRepository = productRepository;
            _autoMapper = autoMapper;
        }
        public async Task<Pagination<ProductToReturnDto>> Handle(GetAllProductWithPaginationQueryV1 request, CancellationToken cancellationToken)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(request.ProductParams);
            var countSpec = new ProductWithFilterForCountSpecification(request.ProductParams);

            var totalItems = await _productRepository.CountAsync(countSpec);

            var products = await _productRepository.ListAsync(spec);

            var data = _autoMapper.Map<IList<ProductToReturnDto>>(products);
            var res = new Pagination<ProductToReturnDto>(request.ProductParams.PageIndex, request.ProductParams.PageSize, totalItems, data);
            return res;




        }
    }
}
