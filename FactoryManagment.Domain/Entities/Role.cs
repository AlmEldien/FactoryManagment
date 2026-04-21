namespace FactoryManagment.Domain.Entities;

public class Role
{
    public string Id { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public bool IsDefault { get; private set; }
    public bool IsDeleted { get; private set; }

    // private Role() { } // For EF if we need it, but since we are not using EF

    public Role(string id, string name, bool isDefault, bool isDeleted)
    {
        // We can add validation logic here or business rules if we needed
        Id = id;
        Name = name;
        IsDefault = isDefault;
        IsDeleted = isDeleted;
    }
}
