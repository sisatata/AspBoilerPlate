using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Demo.Api.Configurations
{
    public static class AutoMapperConfig
    {
        public static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(CleanArch.Demo.Application.AutoMapper.AutoMapperConfiguration));
           // AutoMapperConfiguration.RegisterMappings();
            
        } 
    }
}
