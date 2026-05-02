using FactoryManagment.Application.Abstractions.Repositories;
using FactoryManagment.Domain.Entities;
using FactoryManagment.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace FactoryManagment.Infrastructure.Persistence.Repositories;

internal class ProductionRepository : IProductionRepository
{
    private readonly ApplicationDbContext _context;

    public ProductionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>Returns products that finished within the given period.</summary>
    public async Task<List<Product>> GetFinishedProductsAsync(DateTime start, DateTime end)
    {
        return await _context.Products.AsNoTracking()
            .Where(p => p.Status == Status.Completed &&
                p.FinishedAt >= start &&
                p.FinishedAt <= end)
            .ToListAsync();
    }

    /// <summary>Returns all materials for consumption ratio calculation.</summary>
    public async Task<List<Material>> GetAllMaterialsAsync() => await _context.Materials.AsNoTracking().ToListAsync();

    /// <summary>Returns all machines for capacity and availability calculation.</summary>
    public async Task<List<Machine>> GetAllMachinesAsync() => await _context.Machines.AsNoTracking().ToListAsync();

    /// <summary>
    /// Returns downtimes that overlap with the given period.
    /// Includes downtimes that started before <paramref name="start"/>
    /// or are still ongoing (EndedAt is null).
    /// </summary>
    public async Task<List<MachineDowntime>> GetDowntimesAsync(DateTime start, DateTime end)
    {
        return await _context.MachineDowntimes.AsNoTracking()
            .Where(d => d.StartedAt <= end &&
            (d.EndedAt == null || d.EndedAt >= start))
            .ToListAsync();
    }
}
