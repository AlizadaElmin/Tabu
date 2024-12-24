using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Caching.Memory;
using Tabu.Enums;
using Tabu.Exceptions;
using Tabu.ExternalServices.Abstracts;
using Tabu.ExternalServices.Implements;
using Tabu.Services.Abstracts;
using Tabu.Services.Implements;

namespace Tabu;

public static class ServiceRegistration
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ILanguageService, LanguageService>();
        services.AddScoped<IGameService, GameService>();
        services.AddScoped<IWordService, WordService>();
        services.AddScoped<IBannedWordService, BannedWordService>();
        return services;
    }

    public static IServiceCollection AddCacheService(this IServiceCollection services,IConfiguration _conf,
        CacheTypes type = CacheTypes.Redis)
    {
        if (type == CacheTypes.Redis)
        {
            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = _conf.GetConnectionString("Redis");
                opt.InstanceName = "Tabu_";
            });
            services.AddScoped<ICacheService, RedisService>();
        }
        else
        {
            services.AddMemoryCache();
            services.AddScoped<ICacheService, LocalCacheService>();
        }
        return services;
    }

    public static IApplicationBuilder UseTabuExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(x =>
        {
            x.Run(async context =>
            {
                var feature = context.Features.Get<IExceptionHandlerFeature>();
                Exception exc = feature!.Error;
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                if (exc is IBaseException ibe)
                {
                    context.Response.StatusCode = ibe.StatusCode;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        StatusCode = ibe.StatusCode,
                        Message = ibe.ErrorMessage
                    });
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        StatusCode = StatusCodes.Status400BadRequest,
                        Message = "Bir xəta baş verdi"
                    });
                }
            });

        });
        return app;
    }
}