using CleanArch.Demo.Application.Interfaces;
using CleanArch.Demo.Application.Services;
using CleanArch.Demo.Domain.Commands;
using CleanArch.Demo.Domain.CommnandHandlers;
using CleanArch.Demo.Domain.Core.Bus;
using CleanArch.Demo.Domain.Interfaces;
using CleanArch.Demo.Domain.Models;
using CleanArch.Demo.Infra.Bus;
using CleanArch.Demo.Infra.Core.Interfaces;
using CleanArch.Demo.Infra.Data.Context;
using CleanArch.Demo.Infra.Data.Repository;
using CleanArch.Demo.Infra.Data.Repository.Course;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Infra.Ioc
{
  public  class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
           // services.AddScoped(typeof(IAsyncCourseRepository<Course, Guid>), typeof(CourseRepository));
            services.AddTransient<IMediatorHandler, InMemoryBus>();

            //Domain Handlers
            services.AddScoped<IRequestHandler<CreateCourseCommand, bool>, CourseCommandHandler>();
            services.AddScoped<ICourseService, CourseService>();

            //Infra.Data Layer
            services.AddScoped(typeof(IAsyncRepository<,>), typeof(EfRepository<,>));
            services.AddScoped<UniversityDBContext>();
        }
    }
}
