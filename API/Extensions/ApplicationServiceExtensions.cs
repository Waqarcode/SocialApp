using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.ApiHelper;
using API.Data;
using API.Interfaces;
using API.Repository;
using API.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            //var mapperConfig = new MapperConfiguration(mc =>
            //     {
            //         mc.AddProfile(new AutoMapperProfile());
            //     });

            //     IMapper mapper = mapperConfig.CreateMapper();
            //     services.AddSingleton(mapper);

            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly); 
            services.AddDbContext<DataContext>(options =>{
                
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
                
                });
            
            return services;
        }
    }
}