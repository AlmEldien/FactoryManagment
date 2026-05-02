using FactoryManagment.Application.DTOs;

namespace FactoryManagment.Application.Abstractions.Services;

public interface IDashboardService
{
    /// <summary>
    /// Calculating production KPIs (OEE components) for the given time period.
    /// </summary>
    /// <param name="start">Period start — must be before <paramref name="end"/>.</param>
    /// <param name="end">Period end — cannot be in the future.</param>
    Task<DashboardKpiDto> GetDashboardKpisAsync(DateTime start, DateTime end);
}
