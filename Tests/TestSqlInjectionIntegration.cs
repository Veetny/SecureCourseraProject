
using NUnit.Framework;
using System;
using System.IO;

[TestFixture]
public class TestSqlInjectionIntegration
{
    private SafeVaultDatabase database;
    private string databasePath;

    [SetUp]
    public void SetUp()
    {
        databasePath = Path.Combine(
            Path.GetTempPath(),
            Guid.NewGuid() + ".db"
        );

        database = new SafeVaultDatabase(databasePath);

        database.CreateDatabase();

        database.AddUser("john", "john@example.com");
    }

    [TearDown]
    public void TearDown()
    {
        // Close any pooled SQLite connections.
        database.CloseDatabase();

        // Delete temp database
        if (File.Exists(databasePath))
        {
            File.Delete(databasePath);
        }
    }

    [Test]
    public void NormalUsernameShouldFindExistingUser()
    {
        int result = database.FindUser("john");

        Assert.That(result, Is.EqualTo(1));
    }

    [Test]
    public void SQLInjectionAttemptShouldNotFindAllUsers()
    {
        string suspiciousInput = "' OR '1'='1";

        int result = database.FindUser(suspiciousInput);

        Assert.That(result, Is.EqualTo(0));

        Assert.That(database.FindUser("john"), Is.EqualTo(1));
    }

    [Test]
    public void UnknownUsernameShouldReturnZero()
    {
        int result = database.FindUser("unknown_user");

        Assert.That(result, Is.EqualTo(0));
    }
}