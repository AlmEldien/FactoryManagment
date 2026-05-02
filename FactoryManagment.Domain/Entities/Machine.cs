using FactoryManagment.Domain.ValueObjects;

namespace FactoryManagment.Domain.Entities;

public class Machine
{
    public Guid   Id                 { get; private set; }
    public string Name               { get; private set; }
    public string Description        { get; private set; }
    public int    MaxCapacityPerHour { get; private set; }
    public int    ShiftHours         { get; private set; }

    // Navigation Properties:
    // ======================
    // 1) One Machine "Has" Many Products  (one-to-many with Product)
    public ICollection<Product>         Products  { get; private set; } = [];
    // 2) One Machine "Has" Many Downtimes (one-to-many with MachineDowntimes)
    public ICollection<MachineDowntime> Downtimes { get; private set; } = [];
}
