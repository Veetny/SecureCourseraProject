using System.Collections.Generic;


public class LoginService
{
    private readonly List<User> users;
    private readonly AuthenticationService authentication;

    public LoginService()
    {
        users = new List<User>();
        authentication = new AuthenticationService();
    }

    public void AddUser(string username, string password, string role)
    {
        string passwordHash = authentication.HashPassword(password);

        users.Add(new User(username, passwordHash, role));
    }

    public User? Login(string username, string password)
    {
        // Look for the username in the user list.
        User? user = users.Find(u => u.Username == username);

        // Stop if the username does not exist.
        if (user == null)
        {
            return null;
        }

        // Check whether the password is correct.
        bool passwordIsCorrect = authentication.VerifyPassword(
            password,
            user.PasswordHash
        );

        if (!passwordIsCorrect)
        {
            return null;
        }

        return user;
    }
}