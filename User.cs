public class User
{
    public string Username { get; set; }

    public string PasswordHash { get; set; }

    public string Role { get; set; }

    public User(string username, string passwordHash, string role)
    {
        Username = username;
        PasswordHash = passwordHash;
        Role = role;
    }
}