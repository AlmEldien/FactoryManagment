namespace FactoryManagment.Domain.Entities;

public class ProductMaterial
{
    // Composite Primary Key: ProductId + MaterialId
    public Guid     ProductId    { get; private set; }
    public Guid     MaterialId   { get; private set; }
    public int      QuantityUsed { get; private set; }
    public DateTime AssignedAt   { get; private set; }

    // Navigation Properties:
    // ======================
    // 1) Junction table (one-to-many with both Product & Material) (many-to-many between Product and Material)
    public Product  Product  { get; private set; } = null!;
    public Material Material { get; private set; } = null!;
}
