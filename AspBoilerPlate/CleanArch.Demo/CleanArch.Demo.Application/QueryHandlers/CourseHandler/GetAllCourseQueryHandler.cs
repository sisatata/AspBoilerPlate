using AutoMapper;
using CleanArch.Demo.Application.Interfaces;
using CleanArch.Demo.Application.Queries.CourseQuery;
using CleanArch.Demo.Application.Queries.CourseQuery.Model;
using CleanArch.Demo.Domain.Interfaces;
using CleanArch.Demo.Domain.Models;
using CleanArch.Demo.Shared;
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
    public class GetAllCourseQueryHandler : IRequestHandler<GetAllCourseQueryV1, PagedResponse<List<CourseDto>>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _autoMapper;
        private readonly IDistributedCache _distributedCache;
        private readonly IUriService uriService;
        public GetAllCourseQueryHandler(ICourseRepository courseRepository, IMapper autoMapper, IDistributedCache distributedCache)
        {
            _courseRepository = courseRepository;
            _autoMapper = autoMapper;
            _distributedCache = distributedCache;

        }


        public async Task<PagedResponse<List<CourseDto>>> Handle(GetAllCourseQueryV1 request, CancellationToken cancellationToken)
        {
            /* var cacheKey = "courseList";
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
             return customerList;*/
          
            var validFilter = new PaginationFilter(request.PageNumber, request.PageSize);
            var pagedData = await _courseRepository.GetPagedCourse(validFilter.PageNumber, validFilter.PageSize);
            var data = _autoMapper.Map <List<CourseDto>> (pagedData);
            var totalRecords = await _courseRepository.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<CourseDto>(data, validFilter, totalRecords, request.UriService, request.Path);
           //var data =  _autoMapper.Map<PagedResponse<List<CourseDto>>>(pagedReponse)
            return pagedReponse;






            //  return _autoMapper.Map<List<CourseDto>>(res);

        }
    }
}
