using Microsoft.Playwright;
using NUnit.Framework;
using System.Threading.Tasks;

[TestFixture]
public class TestXssBrowser
{
    [Test]
    public async Task MaliciousHtmlShouldNotExecuteInBrowser()
    {
        // Start browser
        using var playwright = await Playwright.CreateAsync();

        await using var browser =
            await playwright.Chromium.LaunchAsync(
                new BrowserTypeLaunchOptions
                {
                    Headless = true
                }
            );

        var page = await browser.NewPageAsync();

        // Bad input :(
        string userInput =
            "<script>document.body.dataset.xss = 'executed';</script><b>Hello</b>";

        // Use SafeVault encoding
        string safeOutput = OutputEnkoodari.Encode(userInput);

        // Display the content
        await page.SetContentAsync(
            $"<html><body><div id='content'>{safeOutput}</div></body></html>"
        );

        // Check that its seen as text
        string displayedText =
            await page.Locator("#content").InnerTextAsync();

        Assert.That(displayedText, Is.EqualTo(userInput));

        // No scripts
        bool scriptExecuted =
            await page.EvaluateAsync<bool>(
                "() => document.body.dataset.xss === 'executed'"
            );

        Assert.That(scriptExecuted, Is.False);

        // No HTML as elements.
        int scriptCount =
            await page.Locator("#content script").CountAsync();

        Assert.That(scriptCount, Is.EqualTo(0));
    }
}