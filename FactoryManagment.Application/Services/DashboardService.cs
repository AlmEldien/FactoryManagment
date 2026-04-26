using FactoryManagment.Application.DTOs;
using FactoryManagment.Application.Interfaces;
using FactoryManagment.Domain.Entities;
using FactoryManagment.Domain.Enums;
using Microsoft.Extensions.Logging;

namespace FactoryManagment.Application.Services;

public class DashboardService : IDashboardService
{
    private readonly IProductionRepository     _repo;
    private readonly ILogger<DashboardService> _logger;


    public DashboardService(IProductionRepository repo, ILogger<DashboardService> logger)
    {
        _repo   = repo;
        _logger = logger;
    }


    /// <summary>
    /// Calculates production KPIs (OEE components) for the given time period.
    /// </summary>
    /// <param name="start">Period start — must be before <paramref name="end"/>.</param>
    /// <param name="end">Period end — cannot be in the future.</param>
    public async Task<DashboardKpiDto> GetDashboardKpisAsync(DateTime start, DateTime end)
    {
        _logger.LogInformation("KPI calculation started for period {Start:yyyy-MM-dd} → {End:yyyy-MM-dd}", start, end);

        var products = await _repo.GetFinishedProductsAsync(start, end);
        var materials = await _repo.GetAllMaterialsAsync();
        var machines = await _repo.GetAllMachinesAsync();
        var downtimes = await _repo.GetDowntimesAsync(start, end);

        if (products.Count == 0) _logger.LogWarning("No finished products found for this period. Production KPIs will be zero.");
        if (materials.Count == 0) _logger.LogWarning("No materials found in the system. Consumption KPI will be zero.");
        if (machines.Count == 0) _logger.LogWarning("No machines found in the system. OEE calculations will be zero.");
        if (downtimes.Count == 0) _logger.LogInformation("No machine downtimes recorded in this period — full availability assumed.");

        double operatingMinutes = CalculateOperatingMinutes(machines, downtimes, start, end);
        double availability = CalculateAvailability(machines, operatingMinutes);
        double performance = CalculatePerformance(products, machines, operatingMinutes);
        double quality = CalculateQuality(products);
        double oee = Math.Round(availability * performance * quality / 10_000, 2);

        _logger.LogInformation(
            "KPI calculation completed — OEE: {OEE}%, Production: {Count} units, Availability: {Avail}%, Performance: {Perf}%, Quality: {Quality}%",
            oee, products.Count, availability, performance, quality);

        return new DashboardKpiDto(
            TotalProduction: products.Count,
            MaterialConsumption: CalculateConsumption(materials),
            Availability: availability,
            Performance: performance,
            Quality: quality,
            OEE: oee
        );
    }


    // Helper Methods:
    // ===============
    // Operating Time = Planned Time - downtime minutes that overlap with the requested period
    private static double CalculateOperatingMinutes(
        List<Machine> machines,
        List<MachineDowntime> downtimes,
        DateTime periodStart,
        DateTime periodEnd)
    {
        double plannedMinutes = machines.Sum(m => m.ShiftHours) * 60;
        double downtimeMinutes = downtimes.Sum(d => d.GetOverlapMinutes(periodStart, periodEnd));
        return Math.Max(0, plannedMinutes - downtimeMinutes);
    }

    // Availability = Operating Time / Planned Time
    private static double CalculateAvailability(List<Machine> machines, double operatingMinutes)
    {
        double plannedMinutes = machines.Sum(m => m.ShiftHours) * 60;
        if (plannedMinutes == 0) return 0;

        return Math.Round((operatingMinutes / plannedMinutes) * 100, 2);
    }

    // Performance = Actual Output / Ideal Output
    // Ideal Output = sum of MaxCapacityPerHour across all machines × operating hours
    private static double CalculatePerformance(
        List<Product> products,
        List<Machine> machines,
        double operatingMinutes)
    {
        if (operatingMinutes == 0) return 0;

        double idealOutput = machines.Sum(m => m.MaxCapacityPerHour) * (operatingMinutes / 60.0);
        if (idealOutput == 0) return 0;

        return Math.Round(Math.Min((products.Count / idealOutput) * 100, 100), 2);
    }

    // Quality = products with Perfect quality / total finished products
    private static double CalculateQuality(List<Product> products)
    {
        if (products.Count == 0) return 0;

        int goodCount = products.Count(p => p.ProductQuality == ProductQuality.Perfect);
        return Math.Round((goodCount / (double)products.Count) * 100, 2);
    }

    // Material Consumption = total consumed quantity / total available quantity
    private static double CalculateConsumption(List<Material> materials)
    {
        double total = materials.Sum(m => m.Quantity);
        double consumed = materials.Sum(m => m.ConsumedQuantity);
        if (total == 0) return 0;

        return Math.Round((consumed / total) * 100, 2);
    }
}