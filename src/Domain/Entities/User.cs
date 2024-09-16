namespace Domain.Entities;

public class User(string name, string email)
{
    public int Id { get; private set; } 
    public string Name { get; private set; } = name;
    public string Email { get; private set; } = email;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
}