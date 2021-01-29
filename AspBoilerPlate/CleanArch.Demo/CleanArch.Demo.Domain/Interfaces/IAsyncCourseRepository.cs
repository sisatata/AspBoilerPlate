using CleanArch.Demo.Domain.Models;
using CleanArch.Demo.Infra.Core.Interfaces;
using CleanArch.Demo.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArch.Demo.Domain.Interfaces
{
    public interface IAsyncCourseRepository : IAsyncRepository<Course, Guid> 
    {
        
        Task CreateCourse(Course entity);

        Task<Course> GetCourseById(Guid Id);


    }
}
