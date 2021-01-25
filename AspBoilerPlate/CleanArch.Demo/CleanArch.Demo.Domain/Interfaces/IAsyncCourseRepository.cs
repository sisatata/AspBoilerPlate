using CleanArch.Demo.Domain.Models;
using CleanArch.Demo.Infra.Core.Interfaces;
using CleanArch.Demo.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArch.Demo.Domain.Interfaces
{
    public interface IAsyncCourseRepository<T, TId> : IAsyncRepository<Course, int> where T : BaseEntity<int>
    {
        
        Task CreateCourse(Course entity);

        Task<Course> GetCourseById(int Id);


    }
}
