using FactoryManagment.Application.Abstractions.Repositories;
using FactoryManagment.Application.Abstractions.Services;
using FactoryManagment.Application.DTOs;
using FactoryManagment.Domain.Entities;
using FactoryManagment.Domain.Enums;

namespace FactoryManagment.Application.Services;

public class DashboardService : IDashboardService
{
    private readonly IProductionRepository _repo;


    public DashboardService(IProductionRepository repo)
    {
        _repo   = repo;
    }


    /// <summary>
    /// Calculates production KPIs (OEE components) for the given time period.
    /// </summary>
    /// <param name="start">Period start — must be before <paramref name="end"/>.</param>
    /// <param name="end">Period end — cannot be in the future.</param>
    public async Task<DashboardKpiDto> GetDashboardKpisAsync(DateTime start, DateTime end)
    {

        var products = await _repo.GetFinishedProductsAsync(start, end);
        var materials = await _repo.GetAllMaterialsAsync();
        var machines = await _repo.GetAllMachinesAsync();
        var downtimes = await _repo.GetDowntimesAsync(start, end);

        double operatingMinutes = CalculateOperatingMinutes(machines, downtimes, start, end);
        double availability = CalculateAvailability(machines, operatingMinutes);
        double performance = CalculatePerformance(products, machines, operatingMinutes);
        double quality = CalculateQuality(products);
        double oee = Math.Round(availability * performance * quality / 10_000, 2);


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