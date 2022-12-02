using Template.Core.ObjectMapping;
using Template.Data;
using Template.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Template.Core;

public static class ServiceConfigurator
{
    public static IServiceCollection ConfigureServices(
        this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile).Assembly);
        services.AddValidatorsFromAssemblyContaining(typeof(ServiceConfigurator));

        services.ConfigureDataAccess();
        
        services.AddIdentity<User, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        
        return services;
    }
}