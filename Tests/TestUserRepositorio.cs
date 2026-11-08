using NUnit.Framework;
using System.IO;

[TestFixture]
public class TestUserRepositorio
{
    [Test]
    public void UserQueryShouldUseParameters()
    {
        string code = File.ReadAllText(
    Path.Combine(TestContext.CurrentContext.TestDirectory,
    "..", "..", "..", "UserRepository.cs")
);

        Assert.That(code, Does.Contain("@username"));
        Assert.That(code, Does.Contain("Parameters.AddWithValue"));
    }
}