using FactoryManagment.Application.Abstractions.Repositories;
using FactoryManagment.Domain.Entities;
using FactoryManagment.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FactoryManagment.Infrastructure.Persistence.Repositories;

public class AlertRepository : IAlertRepository
{
    private readonly ApplicationDbContext _context;

    public AlertRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Alert>> GetAllAlertsAsync()
    {
        return await _context.Alerts
            .OrderByDescending(a => a.CreatedAt)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Alert>> GetRecentAlertsAsync(int count)
    {
        return await _context.Alerts
            .OrderByDescending(a => a.CreatedAt)
            .Take(count)
            .AsNoTracking()
            .ToListAsync();
    }
}
