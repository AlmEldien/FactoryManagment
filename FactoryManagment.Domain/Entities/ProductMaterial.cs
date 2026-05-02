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


    private ProductMaterial() { }
    private ProductMaterial(Guid productId, Guid materialId, int quantityUsed, DateTime assignedAt)
    {
        ProductId    = productId;
        MaterialId   = materialId;
        QuantityUsed = quantityUsed;
        AssignedAt   = assignedAt;
    }


    // Factory Method to create a new ProductMaterial instance with validation
    public static ProductMaterial Create(Guid productId, Guid materialId, int quantityUsed)
    {
        if (productId == Guid.Empty)
            throw new ArgumentException("Product id is required!", nameof(productId));

        if (materialId == Guid.Empty)
            throw new ArgumentException("Material id is required!", nameof(materialId));

        if (quantityUsed <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantityUsed), "Quantity used must be greater than zero!");

        return new ProductMaterial(productId, materialId, quantityUsed, DateTime.UtcNow);
    }
}
