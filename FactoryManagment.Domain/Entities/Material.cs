using FactoryManagment.Domain.Enums;
using FactoryManagment.Domain.ValueObjects;

namespace FactoryManagment.Domain.Entities;

public class Material
{
    public Guid              Id                { get; private set; }
    public EntityName        Name              { get; private set; } = null!;
    public EntityDescription Description       { get; private set; } = null!;
    public int               Quantity          { get; private set; }
    public int               ConsumedQuantity  { get; private set; }
    public MaterialCondition MaterialCondition { get; private set; }
    public DateTime          EntryAt           { get; private set; }

    // Navigation Properties:
    // ======================
    // 1) One Material "Has" Many Products (many-to-many with Product "Using" ProductMaterial)
    public ICollection<ProductMaterial> ProductMaterials { get; private set; } = [];


    private Material() { }
    private Material(string name, string description, int quantity, int consumedQuantity, MaterialCondition condition, DateTime entryDate)
    {
        Id                = Guid.NewGuid();
        Name              = name;
        Description       = description;
        Quantity          = quantity;
        ConsumedQuantity  = consumedQuantity;
        MaterialCondition = condition;
        EntryAt           = entryDate;
    }


    // Factory Method to create a new Material instance with validation, Value Objects "Name, Description" auto-validated
    public static Material Create(string name, string description, int quantity, int consumedQuantity)
    {
        CheckMaterialQuantity(quantity);
        CheckMaterialConsumedQuantity(consumedQuantity, quantity);

        return new Material(name, description, quantity, consumedQuantity, MaterialCondition.New, DateTime.UtcNow);
    }

    // Update the quantity of an existing material
    public void UpdateQuantity(int quantity)
    {
        CheckMaterialQuantity(quantity);

        if (quantity < ConsumedQuantity)
            throw new ArgumentException("Material quantity can't be less than consumed quantity!", nameof(quantity));

        Quantity = quantity;
    }

    // Update the consumed quantity of an existing material
    public void UpdateConsumedQuantity(int consumedQuantity)
    {
        CheckMaterialConsumedQuantity(consumedQuantity, Quantity);

        ConsumedQuantity = consumedQuantity;
    }


    // Helper Methods:
    // ===============
    // 1) Check material quantity if less than or equal to zero
    private static void CheckMaterialQuantity(int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity), "Material quantity can't be less than or equal to zero!");
    }

    // 2) Check material consumed quantity if less than zero or greater than quantity
    private static void CheckMaterialConsumedQuantity(int consumedQuantity, int quantity)
    {
        if (consumedQuantity < 0)
            throw new ArgumentOutOfRangeException(nameof(consumedQuantity), "Material consumed quantity can't be less than zero!");

        if (consumedQuantity > quantity)
            throw new ArgumentException("Material consumed quantity can't be greater than quantity!", nameof(consumedQuantity));
    }
}
