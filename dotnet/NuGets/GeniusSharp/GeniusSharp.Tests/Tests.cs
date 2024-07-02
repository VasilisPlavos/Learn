namespace GeniusSharp.Tests
{
    // Methodname_Condition_Expectation
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
        [TestCase("Drake")]
        [TestCase("Eminem")]
        public async Task SearchArtist_GiveArtistName_ShouldReturnArtist(string artistName)
        {
            var apiKey = await GetApiKeyAsync();
            var genius = new Genius(apiKey);
            var artist = await genius.SearchArtistAsync(artistName);
            Assert.That(artist?.Name, Is.EqualTo(artistName));
        }

        [Test]
        [Ignore("To save api requests")]
        [TestCase("fgdksjgdhjkshfjkdsd")]
        public async Task SearchArtist_GiveWrongArtistName_ShouldReturnNull(string artistName)
        {
            var apiKey = await GetApiKeyAsync();
            var genius = new Genius(apiKey);
            var artist = await genius.SearchArtistAsync(artistName);
            Assert.That(artist, Is.Null);
        }

        [Test]
        [TestCase("Drake", 130)]
        [TestCase("Eminem", 45)]
        [TestCase("Pek", 113643)] // TODO: Not working! I have to scrape the html and find it there
        public async Task SearchArtistId_GiveArtistName_ShouldReturnArtistId(string artistName, int expectedArtistId)
        {
            var apiKey = await GetApiKeyAsync();
            var genius = new Genius(apiKey);
            var artistId = await genius.SearchArtistIdAsync(artistName);
            Assert.That(artistId, Is.EqualTo(expectedArtistId));
        }

        [Test]
        [Ignore("To save api requests")]
        [TestCase("Drake")]
        public async Task SearchArtist_GiveArtistName_ShouldSaveArtistToLocally(string artistName)
        {
            ClearStorage(true);
            var filePath = Path.Combine(AppContext.BaseDirectory, "storage");

            var filePathExist = Directory.Exists(filePath);
            Assert.That(filePathExist, Is.False);

            await SearchArtist_GiveArtistName_ShouldReturnArtist(artistName);

            filePathExist = Directory.Exists(filePath);
            Assert.That(filePathExist, Is.True);
        }

        [Test]
        [TestCase("Drake", false, 736)]
        [TestCase("Drake", true, 1159)]
        [TestCase("Kodak Black", false, 480)]
        [TestCase("Kodak Black", true, 690)]
        public async Task SearchSongsByArtist_GiveArtistName_ShouldReturnArtistSongs(string artistName, bool includeFeatures, int expectedSongs)
        {

            var apiKey = await GetApiKeyAsync();
            var genius = new Genius(apiKey);
            var songs = await genius.SearchSongsByArtistAsync(artistName, includeFeatures: includeFeatures);
            Assert.That(songs.Count, Is.EqualTo(expectedSongs));
        }

        [Test]
        [TestCase(113643, 25)]
        public async Task GetSongs_GiveArtistId_ShouldReturnArtistSongs(int artistId, int expectedSongs)
        {
            var apiKey = await GetApiKeyAsync();
            var genius = new Genius(apiKey);
            var songs = await genius.GetSongsAsync(artistId, includeFeatures: true);

            Assert.That(songs.Count, Is.EqualTo(expectedSongs));
        }

        private async Task<string> GetApiKeyAsync()
        {
            using var sr = new StreamReader(Path.Combine($"{AppContext.BaseDirectory}", "Files", "access_token.txt"));
            return await sr.ReadToEndAsync();
        }
    }
}