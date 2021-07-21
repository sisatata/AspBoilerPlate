using AutoMapper;
using CleanArch.Demo.Application.Queries.CourseQuery;
using CleanArch.Demo.Application.Queries.CourseQuery.Model;
using CleanArch.Demo.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CleanArch.Demo.Domain.Models;
using System.Data.Common;
using Dapper;
using System.Data;
using System.Linq;

namespace CleanArch.Demo.Application.QueryHandlers.CourseHandler
{
    public class CourseQueryHandler : IRequestHandler<GetCoursesQueryV1, CourseDto>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _autoMapper;
        private readonly DbConnection _dbConnection;
        public CourseQueryHandler(ICourseRepository courseRepository, IMapper autoMapper, DbConnection dbConnection)
        {
            _courseRepository = courseRepository;
            _autoMapper = autoMapper;
            _dbConnection = dbConnection;
        }
        public async Task<CourseDto> Handle(GetCoursesQueryV1 request, CancellationToken cancellationToken)
        {

             var result = await _courseRepository.GetCourseById(request.Id);
            return _autoMapper.Map<CourseDto>(result);

           

        }
    }
}
