﻿using CleanArch.Demo.Domain.Interfaces;
using CleanArch.Demo.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Data.SqlClient;
namespace CleanArch.Demo.Infra.Data.Repository.Course
{
    public class CourseRepository : EfRepository<Domain.Models.Course,Guid>, ICourseRepository
    {
      
        public CourseRepository(UniversityDBContext universityDBContext) : base(universityDBContext)
        {}
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
