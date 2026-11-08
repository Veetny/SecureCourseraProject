using NUnit.Framework;

[TestFixture]
public class TestInputValidation
{
    [Test]
    public void ValidUsernameShouldBeAccepted()
    {
        bool result = InputValidator.IsValidUsername("John123");

        Assert.That(result, Is.True);
    }

    [Test]
    public void InvalidUsernameShouldBeRejected()
    {
        bool result = InputValidator.IsValidUsername("<script>");

        Assert.That(result, Is.False);
    }

    [Test]
    public void SQLInjectionInputShouldBeRejected()
    {
        string username = "' OR '1'='1";

        bool result = InputValidator.IsValidUsername(username);

        Assert.That(result, Is.False);
    }

    [Test]
    public void XSSInputShouldBeRejected()
    {
        string username = "<script>alert('XSS')</script>";

        bool result = InputValidator.IsValidUsername(username);

        Assert.That(result, Is.False);
    }

    [Test]
    public void ValidEmailShouldBeAccepted()
    {
        bool result = InputValidator.IsValidEmail("john@example.com");

        Assert.That(result, Is.True);
    }

    [Test]
    public void InvalidEmailShouldBeRejected()
    {
        bool result = InputValidator.IsValidEmail("notanemail");

        Assert.That(result, Is.False);
    }
    [Test]
public void XSSContentShouldBeEncoded()
{
    string input = "<script>alert('XSS')</script>";

    string result = OutputEnkoodari.Encode(input);

    Assert.That(result, Does.Not.Contain("<script>"));
    Assert.That(result, Does.Contain("&lt;script&gt;"));
}
}