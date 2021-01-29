using CleanArch.Demo.Domain.Interfaces;
using CleanArch.Demo.Infra.Data.Context;
using CleanArch.Demo.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Linq;
using CleanArch.Demo.Domain;
using CleanArch.Demo.Domain.Models;
using Microsoft.Data.SqlClient;

namespace CleanArch.Demo.Infra.Data.Repository.Course
{
    public class CourseRepository : EfRepository<Domain.Models.Course,Guid>, IAsyncCourseRepository
    {
       
        public CourseRepository(UniversityDBContext universityDBContext) : base(universityDBContext)
        {
        }







        

      public async  Task<Domain.Models.Course> GetCourseById(Guid Id)
        {
           

            var data = await _universityDBContext.Courses.FromSqlRaw("spGetCourseById @Id",
               new SqlParameter("Id", Id)).ToListAsync();
      
            return   data.FirstOrDefault();


        }

        

        public async Task CreateCourse(Domain.Models.Course entity)
        {
            await _universityDBContext.AddAsync(entity);
            await _universityDBContext.SaveChangesAsync();

        }

        

        
    }

}
