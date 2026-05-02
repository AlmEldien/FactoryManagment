using FactoryManagment.Domain.Enums;
using FactoryManagment.Domain.Exceptions;
using FactoryManagment.Domain.ValueObjects;

namespace FactoryManagment.Domain.Entities;

public class Product
{
    public Guid              Id                  { get; private set; }
    public EntityName        Name                { get; private set; } = null!;
    public EntityDescription Description         { get; private set; } = null!;
    public Status            Status              { get; private set; }
    public ProductQuality    ProductQuality      { get; private set; }
    public DateTime          StartedProducingAt  { get; private set; }
    public DateTime?         FinishedAt          { get; private set; }
    public DateTime?         AssignedToMachineAt { get; private set; }

    // Navigation Properties:
    // ======================
    // 1) One Product "Has" One Machine  (one-to-many with Machine)
    public Guid?    MachineId { get; private set; }    // Foreign Key
    public Machine? Machine   { get; private set; }
    // 2) One Product "Has" Many Materials (many-to-many with Material "Using" ProductMaterial)
    public ICollection<ProductMaterial> ProductMaterials { get; private set; } = [];


    private Product() { }
    private Product(string name, string description, Status status, ProductQuality productQuality, DateTime startedProducingDate)
    {
        Id                 = Guid.NewGuid();
        Name               = name;
        Description        = description;
        Status             = status;
        ProductQuality     = productQuality;
        StartedProducingAt = startedProducingDate;
    }


    // Factory Method to create a new Product instance with validation, Value Objects "Name, Description" auto-validated
    public static Product Create(string name, string description)
    {
        return new Product(name, description, Status.InProgress, ProductQuality.UnderProduction, DateTime.UtcNow);
    }

    // Set existing product as completed
    public void MarkAsCompleted()
    {
        if (Status != Status.InProgress)
            throw new InvalidProductStateException(Status, $"Can't complete product in {Status.ToString().ToLower()} state!");

        Status = Status.Completed;
        ProductQuality = ProductQuality.Perfect;
        FinishedAt = DateTime.UtcNow;
    }

    // Assign a product to a machine
    public void AssignToMachine(Guid machineId)
    {
        if (Status != Status.InProgress)
            throw new InvalidProductStateException(Status,
                $"Cannot assign a product in {Status.ToString().ToLower()} state to a machine.");

        MachineId = machineId;
        AssignedToMachineAt = DateTime.UtcNow;
    }
}
