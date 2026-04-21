using FactoryManagment.Application.Abstractions.Interfaces;
using FactoryManagment.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FactoryManagment.Api.Controller
{
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet("current-activity")]
        public async Task<IActionResult> GetCurrentActivity()
        {
            var currentActivity = await _dashboardService.GetCurrentActivityAsync();
            return Ok(currentActivity);
        }
    }
}
