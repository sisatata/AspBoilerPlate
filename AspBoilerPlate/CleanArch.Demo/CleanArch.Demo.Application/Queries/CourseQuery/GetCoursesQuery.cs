using AutoMapper;
using CleanArch.Demo.Application.Queries.CourseQuery.Model;

using CleanArch.Demo.Domain.Interfaces;
using CleanArch.Demo.Infra.Core.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Application.Queries.CourseQuery
{
    // if need to cache response plase implement ICacheableMediatrQuery
    public class GetCoursesQuery : IRequest<CourseDto>, ICacheableMediatrQuery
    {
        public Guid Id { get; set; }
        public bool BypassCache { get; set; }
        public string CacheKey => $"Customer-{Id}";
        public TimeSpan? SlidingExpiration { get; set; }
    }
}
