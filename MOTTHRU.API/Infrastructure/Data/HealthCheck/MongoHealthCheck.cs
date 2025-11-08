using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MOTTHRU.API.Infrastructure.Data.AppData;

namespace MOTTHRU.API.Infrastructure.Data.HealthCheck;

public class MongoHealthCheck : IHealthCheck
{
    private readonly ApplicationContext _context;

    public MongoHealthCheck(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            await _context.Moto.AsNoTracking().Take(1).AnyAsync(cancellationToken);

            return HealthCheckResult.Healthy("EF (Mongo) respondeu à consulta.");
        }
        catch (Exception ex)
        {

            return HealthCheckResult.Unhealthy("EF (Mongo) falhou ao consultar.", ex);
        }
    }
}
