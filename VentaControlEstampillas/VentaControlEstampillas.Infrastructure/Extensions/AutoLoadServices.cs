﻿using VentaControlEstampillas.Domain.Ports;
using VentaControlEstampillas.Domain.Services;
using VentaControlEstampillas.Infrastructure.Adapters;
using VentaControlEstampillas.Infrastructure.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace VentaControlEstampillas.Infrastructure.Extensions;

public static class AutoLoadServices
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        // generic repository
        services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>));

        // unit of work
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        // all services with domain service attribute, we can also do this "by convention",
        // naming services with suffix "Service" if decide to remove the domain service decorator
        var _services = AppDomain.CurrentDomain.GetAssemblies()
              .Where(assembly =>
              {
                  return (assembly.FullName is null) || assembly.FullName.Contains("Domain", StringComparison.InvariantCulture);
              })
              .SelectMany(s => s.GetTypes())
              .Where(p => p.CustomAttributes.Any(x => x.AttributeType == typeof(DomainServiceAttribute)));

        // Ditto, but repositories
        var _repositories = AppDomain.CurrentDomain.GetAssemblies()
            .Where(assembly =>
            {
                return (assembly.FullName is null) || assembly.FullName.Contains("Infrastructure", StringComparison.InvariantCulture);
            })
            .SelectMany(s => s.GetTypes())
            .Where(p => p.CustomAttributes.Any(x => x.AttributeType == typeof(RepositoryAttribute)));

        // svc
        foreach (var service in _services)
        {
            services.AddTransient(service);
        }

        // repos
        foreach (var repo in _repositories)
        {
            Type? iface = repo.GetInterfaces().SingleOrDefault();
            if (iface == null)
            {
                throw new InvalidOperationException($"El tipo {repo.Name} no implementa ninguna interfaz o implementa más de una.");
            }
            services.AddTransient(iface, repo);

        }

        return services;
    }
}