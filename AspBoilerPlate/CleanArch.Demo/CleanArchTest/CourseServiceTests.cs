using AutoMapper;
using CleanArch.Demo.Application.Queries.CourseQuery;
using CleanArch.Demo.Application.Queries.CourseQuery.Model;
using CleanArch.Demo.Application.QueryHandlers.CourseHandler;
using CleanArch.Demo.Domain.Interfaces;
using CleanArch.Demo.Domain.Models;
using CleanArch.Demo.Infra.Core.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;


namespace CleanArchTest
{
  public  class CourseServiceTests
    {

        private Mock<IMediator> Mediator;
        private readonly Mock<ICourseRepository> _courseRepository;
        private readonly Mock<IMapper> _autoMapper;
        private readonly Mock<DbConnection> _dbConnection;

        public CourseServiceTests()
        {
            Mediator = new Mock<IMediator>();
            _courseRepository = new Mock<ICourseRepository>();
            _autoMapper = new Mock<IMapper>();
            _dbConnection = new Mock<DbConnection>();
        }
        
        [Fact]
        public  void CourseQueryHandler_ShouldReturnCourse_WhenCourseExist()
        {
            var request = new GetCoursesQueryV1();
            var token = new CancellationToken();
            Mediator.Setup(x => x.Send(It.IsAny<GetCoursesQueryV1>(), new CancellationToken())).
                ReturnsAsync(new CourseDto());
            var orderController = new CourseQueryHandler(_courseRepository.Object, _autoMapper.Object,_dbConnection.Object);
            var res =  orderController.Handle(request, token);
            Assert.IsType<Task<CourseDto>>(res);
            Assert.NotNull(res);
        
        }
    }
}
