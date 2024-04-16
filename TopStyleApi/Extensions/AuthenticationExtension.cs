using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace TopStyleApi.Extensions
{
    public static class AuthenticationExtension
    {
        public static IServiceCollection AddCustomExtension(this IServiceCollection services,  IConfiguration configuration)
        {
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                 .AddJwtBearer(options =>
                 {
                     options.SaveToken = true;
                     options.RequireHttpsMetadata = false;
                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuerSigningKey = true,
                         ValidateAudience = true,
                         ValidateIssuer = true,
                         ValidIssuer = configuration["Appsettings:Issuer"],
                         ValidAudience = configuration["Appsettings:Audience"],
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                                configuration.GetSection("AppSettings:Token").Value!))
                     };
                 });

            return services;
        }
    }
}
