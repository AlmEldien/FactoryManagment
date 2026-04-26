using FactoryManagment.Domain.Enums;

namespace FactoryManagment.Domain.Entities;

public class Alert
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public AlertType Type { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsRead { get; set; }
}
