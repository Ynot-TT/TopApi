using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TopStyle.Core.Interfaces;
using TopStyle.Core.Services;
using TopStyle.Data;
using TopStyle.Data.Interfaces;
using TopStyle.Data.Repos;
using TopStyle.Domain.Auth.Authentication;
using TopStyle.Domain.Auth.Interface;
using TopStyle.Domain.Identity;
using TopStyleApi.Core.Interfaces;
using TopStyleApi.Core.Services;
using TopStyleApi.Data.Interfaces;
using TopStyleApi.Data.Repos;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Filters;

namespace TopStyleApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            builder.Services.AddDbContext<TopStyleContext>(
            options => options.UseSqlServer(@"Data Source=localhost;Initial Catalog=TopStyleDB;Integrated Security=SSPI;TrustServerCertificate=True;"));

            //Swagger
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            //Authentication
            builder.Services.AddAuthentication().AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                            builder.Configuration.GetSection("AppSettings:Token").Value!))
                };
            });
            //Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<TopStyleContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddAuthorization();


            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IUserRepo, UserRepo>();

            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddTransient<IProductRepo, ProductRepo>();

            builder.Services.AddTransient<IOrderService, OrderService>();
            builder.Services.AddTransient<IOrderRepo, OrderRepo>();

            builder.Services.AddTransient<IJwtTokenService, JwtTokenService>();

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
