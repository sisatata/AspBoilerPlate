using AutoMapper;
using CleanArch.Demo.Application.Queries.CourseQuery;
using CleanArch.Demo.Application.Queries.CourseQuery.Model;
using CleanArch.Demo.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArch.Demo.Application.QueryHandlers.CourseHandler
{
    public class GetAllCourseQueryHandler : IRequestHandler<GetAllCourseQuery, List<CourseDto>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _autoMapper;
        private readonly IDistributedCache _distributedCache;
        public GetAllCourseQueryHandler(ICourseRepository courseRepository, IMapper autoMapper, IDistributedCache distributedCache)
        {
            _courseRepository = courseRepository;
            _autoMapper = autoMapper;
            _distributedCache = distributedCache;

        }


        public async Task<List<CourseDto>> Handle(GetAllCourseQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = "courseList";
            string serializedCustomerList;
            var customerList = new List<CourseDto>();
            var redisCustomerList = await _distributedCache.GetAsync(cacheKey);
            if (redisCustomerList != null)
            {
                serializedCustomerList = Encoding.UTF8.GetString(redisCustomerList);
                customerList = JsonConvert.DeserializeObject<List<CourseDto>>(serializedCustomerList);
            }
            else
            {
                var res = await _courseRepository.GetAll();
                serializedCustomerList = JsonConvert.SerializeObject(res);
                redisCustomerList = Encoding.UTF8.GetBytes(serializedCustomerList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await _distributedCache.SetAsync(cacheKey, redisCustomerList, options);
            }
            return customerList;




            
          //  return _autoMapper.Map<List<CourseDto>>(res);

        }
    }
}
