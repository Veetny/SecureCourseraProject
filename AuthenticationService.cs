using BCrypt.Net;

public class AuthenticationService
{
    public string HashPassword(string password)
    {
        // Create a new secure password of the hash (Toimiiko?)
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        // If the password matches the hash
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}