using CleanArch.Demo.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Infra.Data.Context
{
   public class UniversityDBContext : IdentityDbContext
    {
        public UniversityDBContext(DbContextOptions<UniversityDBContext> options) : base(options)
        {
        }

        public  DbSet<Course> Courses { get; set; }
        // ApplicationUser

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    }
}
