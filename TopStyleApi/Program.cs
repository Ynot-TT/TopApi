using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TopStyle.Core.Interfaces;
using TopStyle.Core.Services;
using TopStyle.Data;
using TopStyle.Data.Interfaces;
using TopStyle.Data.Repos;
using TopStyle.Domain.Identity;
using TopStyleApi.Core.Interfaces;
using TopStyleApi.Core.Services;
using TopStyleApi.Data.Interfaces;
using TopStyleApi.Data.Repos;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Filters;
using System.Net;
using TopStyleApi.Extensions;
using TopStyle.Domain.Auth.Interface;
using TopStyle.Domain.Auth.Authentication;

namespace TopStyleApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Azure database
            //username:topstyle
            //password:Admin123

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            builder.Services.AddDbContext<TopStyleContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });

            //Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<TopStyleContext>()
            .AddDefaultTokenProviders();

            //Swagger
            builder.Services.AddCustomSwagger();


            //Authentication
            builder.Services.AddCustomExtension(builder.Configuration);

            
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepo, UserRepo>();

            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IProductRepo, ProductRepo>();

            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IOrderRepo, OrderRepo>();

            builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

            var app = builder.Build();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            
            app.Run();
        }
    }
}
