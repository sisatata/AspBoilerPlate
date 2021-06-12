using AutoMapper;
using CleanArch.Demo.Application.Queries.ProductTypeQuery;
using CleanArch.Demo.Application.Queries.ProductTypeQuery.Model;
using CleanArch.Demo.Domain.Models;
using CleanArch.Demo.Infra.Core.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace CleanArch.Demo.Application.QueryHandlers.ProductTypeQueryHandler
{
    
    public class GetAllTypeQueryHandler : IRequestHandler<GetAllTypeQuery, IList<TypeDto>>
    {
        private readonly IAsyncRepository<ProductType, Guid> _typeRepo;
        private readonly IMapper _autoMapper;
        private readonly IDistributedCache _distributedCache;
        public GetAllTypeQueryHandler(IAsyncRepository<ProductType, Guid> typeRepo, IMapper autoMapper)
        {
            _typeRepo = typeRepo;
            _autoMapper = autoMapper;
        }
        public async Task<IList<TypeDto>> Handle(GetAllTypeQuery request, CancellationToken cancellationToken)
        {
            IList<ProductType> types = await _typeRepo.GetAll();
            IList<TypeDto> data = _autoMapper.Map<List<TypeDto>>(types);
            return data;
            
        }
    }
}
