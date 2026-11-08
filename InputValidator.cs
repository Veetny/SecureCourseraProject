using System.Text.RegularExpressions;

public class InputValidator
{
    public static bool IsValidUsername(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
        {
            return false;
        }

        // A username use only letters and numbers and maybe underscores
        return Regex.IsMatch(username, @"^[a-zA-Z0-9_]{3,30}$");
    }

    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        if (email.Length > 100)
        {
            return false;
        }

        // Is the Email in a good enough format for code?
        return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    }
}