namespace GeniusSharp.Tests
{
    public class Tests
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
        [Ignore("To save api requests")]
        [TestCase("Drake", 130)]
        public async Task LocalStorage_SavingArtists(string artistName, int expectedArtistId)
        {
            ClearStorage(true);
            var filePath = Path.Combine(AppContext.BaseDirectory, "storage");

            var filePathExist = Directory.Exists(filePath);
            Assert.That(filePathExist, Is.False);

            await SearchArtist_ShouldReturnArtistId(artistName, expectedArtistId);

            filePathExist = Directory.Exists(filePath);
            Assert.That(filePathExist, Is.True);
        }

        [Test]
        // TODO:
        [Ignore("Not implemented")]
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