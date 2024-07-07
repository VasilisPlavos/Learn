// Methodname_Condition_Expectation
namespace BrainSharp.NugetCheck.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("BrainSharp.Xml", "1.0.6", 0)]
        [TestCase("BrainSharp.Xml", "1.0.7", 1)]
        [TestCase("coverlet.collector", "3.1.2", 0)]
       // [TestCase("Microsoft.NET.Test.Sdk", "17.3.2", 21)] // skipped because it has 300+ dependencies
      //  [TestCase("MSTest.TestAdapter", "2.2.10", 3)]
      //  [TestCase("MSTest.TestFramework", "2.2.10", 2)]
        public async Task CheckPackageAndTransients_StringContents_ExpectedResult(string packageName, string packageVersion, int expectedWarnings)
        {
            var nugetCheck = new NugetCheck();
            var result = await nugetCheck.CheckPackageAndTransientsAsync(packageName, packageVersion);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.NugetPackageId, Is.EqualTo(packageName));
            Assert.That(result.NugetPackageVersion, Is.EqualTo(packageVersion));
            Assert.That(result.Warnings.Count, Is.EqualTo(expectedWarnings));
            Assert.Pass();
        }

        [Test]
        [TestCase("sixlabors.imagesharp", "3.1.3", true)]
        [TestCase("sixlabors.imagesharp", "3.1.4", false)]
        [TestCase("Newtonsoft.Json", "12.0.3", true)]
        [TestCase("Newtonsoft.Json", "13.0.3", false)]
        [TestCase("System.ServiceModel.Duplex", "4.0.0", true)] // moderate severity
        [TestCase("System.ServiceModel.Duplex", "4.4.0", true)] // high severity
        public async Task IsVulnerable_StringContents_ExpectedResult(string packageName, string packageVersion, bool expected)
        {
            var nugetCheck = new NugetCheck();
            var isVulnerable = await nugetCheck.IsVulnerableAsync(packageName, packageVersion);

            Assert.That(isVulnerable, Is.Not.Null);
            Assert.That(isVulnerable, Is.EqualTo(expected));

            Assert.Pass();
        }

        [Test]
        [TestCase("BrainSharp.Xml", "1.0.6", false)]
        [TestCase("BrainSharp.Xml", "1.0.2", true)]
        public async Task IsDeprecated_StringContents_ExpectedResult(string packageName, string packageVersion, bool expected)
        {
            var nugetCheck = new NugetCheck();
            var isDeprecated = await nugetCheck.IsDeprecatedAsync(packageName, packageVersion);

            Assert.That(isDeprecated, Is.Not.Null);
            Assert.That(isDeprecated, Is.EqualTo(expected));

            Assert.Pass();
        }

        [Test]
        [TestCase("BrainSharp.Xml", "1.0.1", false)]
        [TestCase("BrainSharp.Xml", "1.0.3", false)]
        [TestCase("BrainSharp.Xml", "1.0.6", true)]
        public async Task IsListed_StringContents_ExpectedResult(string packageName, string packageVersion, bool expected)
        {
            var nugetCheck = new NugetCheck();
            var isListed = await nugetCheck.IsListedAsync(packageName, packageVersion);

            Assert.That(isListed, Is.Not.Null);
            Assert.That(isListed, Is.EqualTo(expected));

            Assert.Pass();
        }

        [Test]
        [TestCase("sixlabors.imagesharp")]
        public async Task SearchPackageAsync_GiveName_Return(string packageName)
        {
            var nugetCheck = new NugetCheck();
            var package = await nugetCheck.SearchPackageAsync(packageName);

            Assert.That(package, Is.Not.Null);
            Assert.That(package?.NugetPackageId.ToLower(), Is.EqualTo(packageName.ToLower()));
        }

        [Test]
        [TestCase("sixlabors.imagesharp", "3.1.3")]
        public async Task SearchPackageWithVersionAsync_GiveName_Return(string packageName, string packageVersion)
        {
            var nugetCheck = new NugetCheck();
            var package = await nugetCheck.SearchPackageAsync(packageName);
            var packageVersionInfo = await nugetCheck.SearchPackageVersionInfoAsync(package, packageVersion);

            Assert.That(packageVersionInfo, Is.Not.Null);
            Assert.That(packageVersionInfo?.Version, Is.EqualTo(packageVersion));
        }
    }






}