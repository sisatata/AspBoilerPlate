using CleanArch.Demo.Api.Configurations;
using CleanArch.Demo.Api.ExtensionMethods;
using CleanArch.Demo.Application.Settings;
using CleanArch.Demo.Infra.Data.Context;
using CleanArch.Demo.Infra.Ioc;
using CleanArch.Demo.Shared.Constants.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace CleanArch.Demo.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddServices();
            services.AddOptions();
            services.AddHttpContextAccessor();
            services.AddMemoryCache();
            services.Configure<JWT>(Configuration.GetSection("JWT"));
            services.AddDbContext<UniversityDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("UniversityDBConnection"));
            });

            services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<UniversityDBContext>()
                .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

            /* services.AddTransient<ICourseService, CourseService>();
             services.AddTransient<IAsyncCourseRepository<Domain.Models.Course, int>, CourseRepository<Domain.Models.Course, int>>();
 */
            //   services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<UniversityDBContext>();
            services.AddControllers();
            services.AddAuthorization(options =>
            {
                options.AddPolicy(PolicyTypes.Users.CreateUser, policy => policy.RequireClaim("Permission", Permissions.Users.Create));
               // options.AddPolicy(PolicyTypes.Teams.AddRemove, policy => policy.RequireClaim("Permission", Permissions.Products.Create));
            });
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })

                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = false;
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,

                        ValidIssuer = Configuration["JWT:Issuer"],
                        ValidAudience = Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]))
                    };
                });
            services.AddMemoryCache();
            //


            //
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CleanArch.Demo.Api", Version = "v1" });
            });
            services.RegisterAutoMapper();
            RegisterServices(services);

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
               
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Clean.Arch.Demo v1");
                    options.RoutePrefix = "swagger";
                    options.DisplayRequestDuration();
                });
            }
            
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        private static void RegisterServices(IServiceCollection services)
        {
            DependencyContainer.RegisterServices(services);
        }
    }
}
