using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

namespace TopStyleApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddAutoMapper(typeof(Program).Assembly);


            builder.Services.AddDbContext<TopStyleContext>(
            options => options.UseSqlServer(@"Data Source=localhost;Initial Catalog=TopStyleDB;Integrated Security=SSPI;TrustServerCertificate=True;"));

            //Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<TopStyleContext>()
            .AddDefaultTokenProviders();

            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddTransient<IUserRepo, UserRepo>();

            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddTransient<IProductRepo, ProductRepo>();

            builder.Services.AddTransient<IJwtTokenService, JwtTokenService>();

            

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAuthorization();

            var app = builder.Build();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwagger();
            app.UseSwaggerUI();
            app.Run();
        }
    }
}
