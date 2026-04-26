namespace FactoryManagment.Application.Requests;

// Request model received from the client and validated in the Application layer (using FluentValidation)
// It belongs conceptually to the API layer but is placed here because it's processed and validated in the Application layer
public record DashboardKpiRequest(DateTime Start, DateTime End);
