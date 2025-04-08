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
    [TestCase(8.625334, 47.473335, 47.281473, 8.447982, 1000, 1000)]
    [TestCase(8.625334, 47.473335, 47.281473, 8.447982, 10, 10)]
    public async Task GetPins(double east, double north, double south, double west, int maxCount, int expectedCount)
    {
        var flatFox = new FlatfoxService();

        var request = new GetPinsRequestDto
        {
            East = east,
            North = north,
            South = south,
            West = west,
            MaxCount = maxCount
        };

        var pins = await flatFox.GetPinsAsync(request);
        Assert.That(pins, Is.Not.Null);
        Assert.That(pins.Count, Is.EqualTo(expectedCount));

        var pin = pins.FirstOrDefault();
        Assert.That(pin, Is.Not.Null);
        Assert.That(pin.latitude, Is.AtMost(north));
        Assert.That(pin.longitude, Is.AtMost(east));
    }

    [Test]
    [TestCase(new[] { 1639421, 1639418 })]
    [TestCase(new[] { 1640119, 1640116, 1640115, 1640114, 1640108, 1640106, 1640087, 1640080, 1640078, 1640073, 1640071, 1640058, 1640016, 1639667, 1639656, 1639648, 1639637, 1639626, 1639621, 1639604, 1639594, 1639568, 1639567, 1639566, 1639563, 1639562, 1639561, 1639558, 1310696, 1639546, 1638011, 1639537, 1639509, 1639499, 1639495, 1639490, 1634782, 1629988, 1639476, 1639466, 1639447, 1639441, 1639431, 1639425, 1639423, 1457037, 1639421, 1639418 })]
    public async Task GetListing(int[] idArray)
    {
        var flatFox = new FlatfoxService();
        List<int> ids = idArray.ToList();

        var listings = await flatFox.GetListingsAsync(ids);

        Assert.That(listings, Is.Not.Null);
        Assert.That(listings.Count, Is.EqualTo(ids.Count));
    }
}