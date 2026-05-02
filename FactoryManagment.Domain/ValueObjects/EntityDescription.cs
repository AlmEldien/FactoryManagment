namespace FactoryManagment.Domain.ValueObjects;

public record EntityDescription
{
    public string Value { get; }

    public EntityDescription(string description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description is required!", nameof(description));

        if (description.Length > 1000)
            throw new ArgumentException("Description can't exceed 1000 characters!", nameof(description));

        Value = description.Trim();
    }

    public static implicit operator string(EntityDescription description) => description.Value;
    public static implicit operator EntityDescription(string description) => new(description);

    public override string ToString() => Value.ToString();
}
