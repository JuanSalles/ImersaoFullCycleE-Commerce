using AuthServer.Application.Services;
using AuthServer.Core.Entities;
using AuthServer.Core.Interfaces;
using AuthServer.Infra;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

namespace AuthServer.WebApplication;

public static class ServiceColletionExtensions
{
    public static void ServiceBuilder(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
    }

    public static void DbContextBuilder(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<UserDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
    }

    public static void IdentityBuilder(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<UserDbContext>()
                .AddDefaultTokenProviders();
    }

    public static void AutheticationBuilder(this IServiceCollection services, IConfiguration configuration)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSecret"]));

        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["JWTIssuer"],
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ClockSkew = TimeSpan.Zero
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = context =>
                        {   
                            context.HandleResponse();
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";
                            var result = JsonSerializer.Serialize(new { message = "You are not Authorized" });
                            return context.Response.WriteAsync(result);
                        }
                    };

                });
    }


}