using AutoMapper;
using CleanArch.Demo.Application.Queries.ProductQuery;
using CleanArch.Demo.Application.Queries.ProductQuery.Model;
using CleanArch.Demo.Domain.Models;
using CleanArch.Demo.Infra.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArch.Demo.Application.QueryHandlers.BrandHandler
{
    public class GetAllBrandQueryHandler : IRequestHandler<GetAllBrandQuery, List<BrandDto>>
    {
        private readonly  IAsyncRepository<ProductBrand,Guid> _productBrandRepo;
        private readonly IMapper _autoMapper;
        private readonly IDistributedCache _distributedCache;

        public GetAllBrandQueryHandler(IAsyncRepository<ProductBrand, Guid> productBrandRepo, IMapper autoMapper)
        {
            _productBrandRepo = productBrandRepo;
            _autoMapper = autoMapper;

        }
        public  async Task<List<BrandDto>> Handle(GetAllBrandQuery request, CancellationToken cancellationToken)
        {
            var brands = await _productBrandRepo.GetAll();
            var data = _autoMapper.Map<List<BrandDto>>(brands);
            return data;


        }
    }
}
