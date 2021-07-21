using CleanArch.Demo.Application.Queries.ProductTypeQuery.Model;
using CleanArch.Demo.Infra.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Application.Queries.ProductTypeQuery
{
   public class GetAllTypeQueryV1 : IRequest<IList<TypeDto>>, ICacheableMediatrQuery
    {
        public int Id { get; set; }
        public bool BypassCache { get; set; }
        public string CacheKey => $"Customer-{Id}";
        public TimeSpan? SlidingExpiration { get; set; }
    }
}
