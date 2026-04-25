namespace FactoryManagment.Domain.ValueObjects;

public record EntityName
{
    public string Value { get; }

    public EntityName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required!", nameof(name));

        if (name.Length > 200)
            throw new ArgumentException("Name can't exceed 200 characters!", nameof(name));

        Value = name.Trim();
    }

    public static implicit operator string(EntityName name) => name.Value;
    public static implicit operator EntityName(string name) => new(name);

    public override string ToString() => Value.ToString();
}