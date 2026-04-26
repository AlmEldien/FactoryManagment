using FactoryManagment.Domain.Entities;

namespace FactoryManagment.Application.Abstractions.Repositories;

public interface IAlertRepository
{
    Task<IEnumerable<Alert>> GetAllAlertsAsync();
    Task<IEnumerable<Alert>> GetRecentAlertsAsync(int count);
}
