using FactoryManagment.Domain.Enums;
using FactoryManagment.Domain.ValueObjects;

namespace FactoryManagment.Domain.Entities;

public class Material
{
    public Guid              Id                { get; private set; }
    public string            Name              { get; private set; }
    public string            Description       { get; private set; }
    public int               Quantity          { get; private set; }
    public int               ConsumedQuantity  { get; private set; }
    public MaterialCondition MaterialCondition { get; private set; }
    public DateTime          EntryAt           { get; private set; }

    // Navigation Properties:
    // ======================
    // 1) One Material "Has" Many Products (many-to-many with Product "Using" ProductMaterial)
    public ICollection<ProductMaterial> ProductMaterials { get; private set; } = [];
}
