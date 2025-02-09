using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

[Route("api/health")]
[ApiController]
public class HealthCheckController : ControllerBase
{
    private readonly HealthCheckService _healthCheckService;

    public HealthCheckController(HealthCheckService healthCheckService)
    {
        _healthCheckService = healthCheckService;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var report = await _healthCheckService.CheckHealthAsync();
        var status = report.Status == HealthStatus.Healthy ? "Healthy" : "Unhealthy";

        return Ok(new 
        { 
            status, 
            timestamp = DateTime.UtcNow, 
            checks = report.Entries.Select(e => new 
            { 
                component = e.Key, 
                status = e.Value.Status.ToString(), 
                description = e.Value.Description 
            }) 
        });
    }
}
