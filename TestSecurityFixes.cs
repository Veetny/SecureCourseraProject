using NUnit.Framework;

[TestFixture]
public class TestSecurityFixes
{
    [Test]
    public void ScriptTagsShouldBeEncoded()
    {
        string input = "<script>alert('XSS')</script>";

        string result = OutputEnkoodari.Encode(input);

        Assert.That(result, Does.Not.Contain("<script>"));
        Assert.That(result, Does.Contain("&lt;script&gt;"));
    }

    [Test]
    public void HTMLTagsShouldBeEncoded()
    {
        string input = "<h1>Hello</h1>";

        string result = OutputEnkoodari.Encode(input);

        Assert.That(result, Does.Not.Contain("<h1>"));
        Assert.That(result, Does.Contain("&lt;h1&gt;"));
    }

    [Test]
    public void NormalTextShouldStillWork()
    {
        string input = "Welcome to SafeVault";

        string result = OutputEnkoodari.Encode(input);

        Assert.That(result, Is.EqualTo(input));
    }

    [Test]
    public void NullInputShouldReturnEmptyString()
    {
        string result = OutputEnkoodari.Encode(null);

        Assert.That(result, Is.EqualTo(string.Empty));
    }

    
[Test]
public void SQLInjectionInputShouldBeSafelyEncoded()
{
    string input = "' OR '1'='1";

    string result = OutputEnkoodari.Encode(input);

    Assert.That(result, Does.Contain("&#39;"));
    Assert.That(result, Does.Not.Contain("'"));
}
}