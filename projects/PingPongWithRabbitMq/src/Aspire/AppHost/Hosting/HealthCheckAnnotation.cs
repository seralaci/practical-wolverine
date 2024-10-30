using Microsoft.Extensions.Diagnostics.HealthChecks;

// ReSharper disable once CheckNamespace
namespace Aspire.Hosting;

/// <summary>
/// An annotation that associates a health check factory with a resource
/// </summary>
/// <param name="healthCheckFactory">A function that creates the health check</param>
public class HealthCheckAnnotation(Func<IResource, CancellationToken, Task<IHealthCheck?>> healthCheckFactory) : IResourceAnnotation
{
    public Func<IResource, CancellationToken, Task<IHealthCheck?>> HealthCheckFactory { get; } = healthCheckFactory;

    public static HealthCheckAnnotation Create(Func<string, IHealthCheck> connectionStringFactory)
    {
        return new HealthCheckAnnotation(async (resource, token) =>
        {
            if (resource is not IResourceWithConnectionString c)
            {
                return null;
            }

            return await c.GetConnectionStringAsync(token) is not { } cs
                ? null
                : connectionStringFactory(cs);
        });
    }
}
