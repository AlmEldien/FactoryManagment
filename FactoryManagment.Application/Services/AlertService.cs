using FactoryManagment.Application.Abstractions.Repositories;
using FactoryManagment.Application.Abstractions.Services;
using FactoryManagment.Application.DTOs;
using FactoryManagment.Domain.Entities;

namespace FactoryManagment.Application.Services;

public class AlertService : IAlertService
{
    private readonly IAlertRepository _alertRepository;

    public AlertService(IAlertRepository alertRepository)
    {
        _alertRepository = alertRepository;
    }

    public async Task<IEnumerable<AlertDto>> GetAllAlertsAsync()
    {
        var alerts = await _alertRepository.GetAllAlertsAsync();
        return alerts.Select(MapToDto);
    }

    public async Task<IEnumerable<AlertDto>> GetRecentAlertsAsync(int count)
    {
        var alerts = await _alertRepository.GetRecentAlertsAsync(count);
        return alerts.Select(MapToDto);
    }

    private static AlertDto MapToDto(Alert alert)
    {
        return new AlertDto
        {
            Id = alert.Id,
            Title = alert.Title,
            Message = alert.Message,
            Type = alert.Type.ToString(),
            CreatedAt = alert.CreatedAt,
            IsRead = alert.IsRead
        };
    }
}
