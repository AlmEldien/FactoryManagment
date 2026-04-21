namespace FactoryManagment.Domain.Entities;

public class RefreshToken
{
    public string Token { get; private set; } = default!;
    public DateTime ExpiresAt { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? RevokedOn { get; private set; }
    public bool IsRevoked { get; private set; }
    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    public bool IsActive => !IsRevoked && !IsExpired;

    private RefreshToken() { } // For EF

    public RefreshToken(string token, DateTime expiresOn, DateTime createdOn)
    {
        Token = token;
        ExpiresAt = expiresOn;
        CreatedAt = createdOn;
    }

    public void Revoke()
    {
        IsRevoked = true;
        RevokedOn = DateTime.UtcNow;
    }
}
