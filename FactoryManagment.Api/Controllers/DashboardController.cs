using FactoryManagment.Application.Abstractions.Services;
using FactoryManagment.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace FactoryManagment.Api.Controllers;


[ApiController]
[Route("api/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly IAlertService _alertService;

    public DashboardController(IAlertService alertService)
    {
        _alertService = alertService;
    }

    /// <summary>
    /// Returns system notifications and machine stoppage alerts for the dashboard.
    /// </summary>
    [HttpGet("alerts")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAlerts([FromQuery] int? count)
    {
        var alerts = count.HasValue
            ? await _alertService.GetRecentAlertsAsync(count.Value)
            : await _alertService.GetAllAlertsAsync();

        return Ok(alerts);
    }
}
