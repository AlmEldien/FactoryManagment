using FactoryManagment.Api.Common;
using FactoryManagment.Application.Abstractions.Services;
using FactoryManagment.Application.DTOs;
using FactoryManagment.Application.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace FactoryManagment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService               _dashboardService;
        private readonly IValidator<DashboardKpiRequest> _kpisValidator;

        public DashboardController(IDashboardService dashboardService, IValidator<DashboardKpiRequest> kpisValidator)
        {
            _dashboardService = dashboardService;
            _kpisValidator    = kpisValidator;
        }


        // Returns production KPIs for date range.
        [HttpGet("production-kpis")]
        public async Task<IActionResult> GetProductionKpis([FromQuery] DashboardKpiRequest request)
        {
            var validation = await _kpisValidator.ValidateAsync(request);

            if (!validation.IsValid)
                return BadRequest(ApiResponse<DashboardKpiDto>.Fail(validation.Errors.Select(e => e.ErrorMessage)));

            var result = await _dashboardService.GetDashboardKpisAsync(request.Start, request.End);
            return Ok(ApiResponse<DashboardKpiDto>.Ok(result));
        }

    }
}
