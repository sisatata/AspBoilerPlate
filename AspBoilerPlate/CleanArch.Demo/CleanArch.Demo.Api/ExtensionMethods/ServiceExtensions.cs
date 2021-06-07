
using CleanArch.Demo.Api.Services;
using CleanArch.Demo.Application.Commands.Model;
using CleanArch.Demo.Application.Interfaces;
using CleanArch.Demo.Application.Services;
using CleanArch.Demo.Domain.Commands;
using CleanArch.Demo.Domain.CommnandHandlers;
using CleanArch.Demo.Domain.Interfaces;
using CleanArch.Demo.Domain.Models;
using CleanArch.Demo.Infra.Core.Interfaces;
using CleanArch.Demo.Infra.Data.Context;
using CleanArch.Demo.Infra.Data.Repository;
using CleanArch.Demo.Infra.Data.Repository.Course;
using CleanArch.Demo.Infra.Data.Repository.Product;
using CleanArch.Demo.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace CleanArch.Demo.Api.ExtensionMethods
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<UniversityDBContext>().AddDefaultTokenProviders();
            services.AddMediatR(typeof(CreateCourseCommand).GetTypeInfo().Assembly);
           
            services.AddControllers();

            services.AddMediatR(Assembly.GetExecutingAssembly());
            // services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            //services.AddSingleton<IMediatorHandler, InMemoryBus>();
            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequiredLength = 5;
                opt.Password.RequireLowercase = true;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(30);
                opt.Lockout.MaxFailedAccessAttempts = 3;
            });
            //Domain Handlers
            services.AddScoped<IRequestHandler<CreateCourseCommand, CommonResponseDto>, CourseCommandHandler>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IUserService, UserService>();
            // services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            services.AddScoped(typeof(IAsyncRepository<,>), typeof(EfRepository<,>));
            services.AddScoped(typeof(List<Course>), typeof(List<Course>));

            // services.AddTransient<ICourseService, CourseService>();
            // services.AddScoped(typeof(IAsyncCourseRepository<Course, Guid>), typeof(CourseRepository));

            // services.Add(new ServiceDescriptor(typeof(IAsyncCourseRepository<Course, Guid>), typeof(CourseRepository)));
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();


        }
    }
};
