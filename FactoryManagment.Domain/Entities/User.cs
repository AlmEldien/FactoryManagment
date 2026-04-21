namespace FactoryManagment.Domain.Entities;

public class User
{
    public string Id { get; private set; } = default!;
    public string FirstName { get; private set; } = default!;
    public string LastName { get; private set; } = default!;
    public string FullName => $"{FirstName} {LastName}";
    public string UserName { get; private set; } = default!;
    public string Email { get; private set; } = default!;
    public string PhoneNumber { get; private set; } = default!;

    // private User() { } // For EF if we need it, but since we are not using EF

    public User(string id, string firstName, string lastName, string userName, string email, string phoneNumber)
    {
        // We can add validation logic here or business rules if we needed
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        Email = email;
        PhoneNumber = phoneNumber;
    }
}
