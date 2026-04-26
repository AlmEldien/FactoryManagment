namespace FactoryManagment.Application.DTOs;

// Represents KPIs values to be sent to "DashboardService" and wrapped by "ApiResponse" in API layer
public record DashboardKpiDto(
    int TotalProduction,
    double MaterialConsumption,
    double Availability,
    double Performance,
    double Quality,
    double OEE
);
