using FactoryManagment.Application.DTOs;

namespace FactoryManagment.Application.Abstractions.Services;

public interface IAlertService
{
    Task<IEnumerable<AlertDto>> GetAllAlertsAsync();
    Task<IEnumerable<AlertDto>> GetRecentAlertsAsync(int count);
}
