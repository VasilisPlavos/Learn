namespace GeniusSharp.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup() { }

        [Test]
        [TestCase("Drake", 130)]
        [TestCase("Eminem", 45)]
        public async Task SearchArtist_ShouldReturnArtistId(string artistName, int expectedArtistId)
        {
            var apiKey = await GetApiKeyAsync();
            var genius = new Genius(apiKey);
            var artistId = await genius.SearchArtistIdAsync(artistName);
            Assert.That(artistId, Is.EqualTo(expectedArtistId));
        }

        [Test]
        public async Task SearchSongsByArtist_ShouldReturnArtist()
        {
            var artistName = "Eminem";

            var Genius = new Genius("");
            var artist = await Genius.SearchSongsByArtistAsync(artistName, 3, "my name is", true);
        }

        private async Task<string> GetApiKeyAsync()
        {
            using var sr = new StreamReader(Path.Combine($"{AppContext.BaseDirectory}","Files", "access_token.txt"));
            return await sr.ReadToEndAsync();
        }
    }
}