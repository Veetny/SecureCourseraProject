using System.Net;

public class OutputEnkoodari
{
    public static string Encode(string? userInput)
    {
        if (userInput == null)
        {
            return string.Empty;
        }

        // Change HTML to safe chars
        return WebUtility.HtmlEncode(userInput);
    }
}