using Y25.ManyProcessors.Dtos.Flatfox;
using Y25.ManyProcessors.Processors;

namespace Y25.Tests;

// Methodname_Condition_Expectation

public class FlatfoxTests
{
    [SetUp]
    public void Setup()
    {
        ClearStorage(false);
    }

    private void ClearStorage(bool clear)
    {
        if (clear == false) return;

        var filePath = Path.Combine(AppContext.BaseDirectory, "storage");
        if (Directory.Exists(filePath)) Directory.Delete(filePath, true);
    }

    [Test]
    [TestCase("8.625334", "47.473335", "47.281473", "8.447982", 1000, 1000)]
    [TestCase("8.625334", "47.473335", "47.281473", "8.447982", 10, 10)]
    public async Task GetPins(string east, string north, string south, string west, int maxCount, int expectedCount)
    {
        var flatFox = new FlatfoxService();
        var pins = await flatFox.GetPinsAsync(east, north, south, west, maxCount);

        Assert.That(pins, Is.Not.Null);
        Assert.That(pins.Count, Is.EqualTo(expectedCount));
    }
}