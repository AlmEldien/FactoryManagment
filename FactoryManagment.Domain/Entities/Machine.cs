using FactoryManagment.Domain.ValueObjects;

namespace FactoryManagment.Domain.Entities;

public class Machine
{
    public Guid              Id                 { get; private set; }
    public EntityName        Name               { get; private set; } = null!;
    public EntityDescription Description        { get; private set; } = null!;
    public int               MaxCapacityPerHour { get; private set; }
    public int               ShiftHours         { get; private set; }

    // Navigation Properties:
    // ======================
    // 1) One Machine "Has" Many Products  (one-to-many with Product)
    public ICollection<Product>         Products  { get; private set; } = [];
    // 2) One Machine "Has" Many Downtimes (one-to-many with MachineDowntimes)
    public ICollection<MachineDowntime> Downtimes { get; private set; } = [];


    private Machine() { }
    private Machine(string name, string description, int maxCapacityPerHour, int shiftHours)
    {
        Id                 = Guid.NewGuid();
        Name               = name;
        Description        = description;
        MaxCapacityPerHour = maxCapacityPerHour;
        ShiftHours         = shiftHours;
    }


    // Factory Method to create a new Machine instance with validation, Value Objects "Name, Description" auto-validated
    public static Machine Create(string name, string description, int maxCapacityPerHour, int shiftHours)
    {
        CheckMachineMaxCapacity(maxCapacityPerHour);
        CheckMachineShiftHours(shiftHours);

        return new Machine(name, description, maxCapacityPerHour, shiftHours);
    }

    // In real life, we change both of "Capacity Per Hour" and "Shift Hours" together
    public void UpdateMaxCapacityPerHourAndShiftHours(int newMaxCapacityPerHour, int newShiftHours)
    {
        CheckMachineMaxCapacity(newMaxCapacityPerHour);
        CheckMachineShiftHours(newShiftHours);

        MaxCapacityPerHour = newMaxCapacityPerHour;
        ShiftHours = newShiftHours;
    }


    // Helper Methods:
    // ===========================
    // 1) Check machine max capacity if less than or equal to zero
    private static void CheckMachineMaxCapacity(int maxCapacityPerHour)
    {
        if (maxCapacityPerHour <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxCapacityPerHour), "Machine max capacity per hour must be greater than zero!");
    }

    // 2) Check machine shift hours if less than 1 or greater than 24
    private static void CheckMachineShiftHours(int shiftHours)
    {
        if (shiftHours is < 1 or > 24)
            throw new ArgumentOutOfRangeException(nameof(shiftHours), "Machine shift hours must be between 1 and 24!");
    }
}
