using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Final_Edi3.Data;

public class BaseDeDatosHealthCheck : IHealthCheck
{
    private readonly IDbContextFactory<TiendaDbContext> _factory;

    public BaseDeDatosHealthCheck(IDbContextFactory<TiendaDbContext> factory)
        => _factory = factory;

    public async Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            await using var db = await _factory.CreateDbContextAsync(cancellationToken);
            return await db.Database.CanConnectAsync(cancellationToken)
                ? HealthCheckResult.Healthy("Base de datos disponible")
                : HealthCheckResult.Unhealthy("No se puede conectar a la base de datos");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy("Error al consultar la base de datos", ex);
        }
    }
}