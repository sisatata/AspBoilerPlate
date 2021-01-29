using CleanArch.Demo.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Infra.Data.Context
{
   public class UniversityDBContext : DbContext
    {
        public UniversityDBContext(DbContextOptions<UniversityDBContext> options) : base(options)
        {
        }

       // public  DbSet<Course> Courses { get; set; }
        public DbSet<Course> Courses { get; set; }
        // ApplicationUser Course

        // public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    }
}
