using AutoMapper;
using CleanArch.Demo.Application.Queries.CourseQuery;
using CleanArch.Demo.Application.Queries.CourseQuery.Model;
using CleanArch.Demo.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Data.Common;

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
