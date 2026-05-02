using FactoryManagment.Domain.Enums;
using FactoryManagment.Domain.Exceptions;
using FactoryManagment.Domain.ValueObjects;

namespace FactoryManagment.Domain.Entities;

public class Product
{
    public Guid           Id                  { get; private set; }
    public string         Name                { get; private set; }
    public string         Description         { get; private set; }
    public Status         Status              { get; private set; }
    public ProductQuality ProductQuality      { get; private set; }
    public DateTime       StartedProducingAt  { get; private set; }
    public DateTime?      FinishedAt          { get; private set; }
    public DateTime?      AssignedToMachineAt { get; private set; }

    // Navigation Properties:
    // ======================
    // 1) One Product "Has" One Machine  (one-to-many with Machine)
    public Guid?    MachineId { get; private set; }    // Foreign Key
    public Machine? Machine   { get; private set; }
    // 2) One Product "Has" Many Materials (many-to-many with Material "Using" ProductMaterial)
    public ICollection<ProductMaterial> ProductMaterials { get; private set; } = [];
}
