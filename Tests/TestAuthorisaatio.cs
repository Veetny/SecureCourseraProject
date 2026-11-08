using NUnit.Framework;

[TestFixture]
public class TestAuthorisaatio
{
    private RoleAuthorization authorization;
    private User admin;
    private User normalUser;

    [SetUp]
    public void SetUp()
    {
        authorization = new RoleAuthorization();

        admin = new User("admin", "hashedPassword", "Admin");
        normalUser = new User("john", "hashedPassword", "User");
    }

    [Test]
    public void AdminShouldAccessAdminDashboard()
    {
        bool result = authorization.CanAccessAdminDashboard(admin);

        Assert.That(result, Is.True);
    }

    [Test]
    public void NormalUserShouldNotAccessAdminDashboard()
    {
        bool result = authorization.CanAccessAdminDashboard(normalUser);

        Assert.That(result, Is.False);
    }

    [Test]
    public void NormalUserShouldAccessUserDashboard()
    {
        bool result = authorization.CanAccessUserDashboard(normalUser);

        Assert.That(result, Is.True);
    }

    [Test]
    public void AdminShouldAccessUserDashboard()
    {
        bool result = authorization.CanAccessUserDashboard(admin);

        Assert.That(result, Is.True);
    }

    [Test]
    public void UnknownUserShouldNotAccessAdminDashboard()
    {
        bool result = authorization.CanAccessAdminDashboard(null);

        Assert.That(result, Is.False);
    }
    [Test]
public void UnknownRoleShouldNotAccessAdminDashboard()
{
    User guest = new User(
        "guest",
        "hashedPassword",
        "Guest"
    );

    bool result =
        authorization.CanAccessAdminDashboard(guest);

    Assert.That(result, Is.False);
}
}