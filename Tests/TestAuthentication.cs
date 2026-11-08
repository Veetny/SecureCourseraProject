using NUnit.Framework;

[TestFixture]
public class TestAuthentication
{
    private LoginService loginService;

    [SetUp]
    public void SetUp()
    {
        loginService = new LoginService();

        loginService.AddUser("admin", "AdminPass123!", "Admin");
        loginService.AddUser("john", "UserPass123!", "User");
    }

    [Test]
    public void CorrectPasswordShouldAllowLogin()
    {
       User? user = loginService.Login("admin", "AdminPass123!");

        Assert.That(user, Is.Not.Null);
    }

    [Test]
    public void WrongPasswordShouldRejectLogin()
    {
        User? user = loginService.Login("admin", "WrongPassword");

        Assert.That(user, Is.Null);
    }

    [Test]
    public void UnknownUsernameShouldRejectLogin()
    {
        User? user = loginService.Login("unknown", "Password123!");

        Assert.That(user, Is.Null);
    }

    [Test]
    public void NormalUserShouldBeAbleToLogin()
    {
        User? user = loginService.Login("john", "UserPass123!");

        Assert.That(user, Is.Not.Null);
        Assert.That(user.Role, Is.EqualTo("User"));
    }
}