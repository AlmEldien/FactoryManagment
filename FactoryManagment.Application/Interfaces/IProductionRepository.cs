using FactoryManagment.Domain.Entities;

namespace FactoryManagment.Application.Interfaces;

// This one interface contains operations on 4 tables instead of creating four separate interfaces
// and injecting them into "DashboardService.cs", one specified is better
// No pagination is provided because the full numeric results
// are returned and will be used to calculate Production KPIs (not for direct display)
public interface IProductionRepository
{
    Task<List<Product>> GetFinishedProductsAsync(DateTime start, DateTime end);
    Task<List<Material>> GetAllMaterialsAsync();
    Task<List<Machine>> GetAllMachinesAsync();
    Task<List<MachineDowntime>> GetDowntimesAsync(DateTime start, DateTime end);
}
