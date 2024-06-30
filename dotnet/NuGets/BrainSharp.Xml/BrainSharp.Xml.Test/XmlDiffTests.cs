using System.Xml;
using BrainSharp.XmlDiff.XmlDiffPatch;

namespace BrainSharp.Xml.Test;

public class XmlDiffTests
{
    [Test]
    public void XmlDiff_Compare_IsEqual()
    {
        // Arrange
        var xml1 = XmlReader.Create($"{AppContext.BaseDirectory}\\Files\\products1.xml");
        var xml2 = XmlReader.Create($"{AppContext.BaseDirectory}\\Files\\products1.xml");

        var diffOpts = XmlDiffOptions.IgnoreNamespaces | XmlDiffOptions.IgnoreComments;
        var diff = new XmlDiff.XmlDiffPatch.XmlDiff(diffOpts);

        // Act
        var isEqual = diff.Compare(xml1, xml2);

        // Assert
        Assert.IsTrue(isEqual);
    }

    [Test]
    public void XmlDiff_Compare_IsNotEqual()
    {
        // Arrange
        var xml1 = XmlReader.Create($"{AppContext.BaseDirectory}\\Files\\products1.xml");
        var xml2 = XmlReader.Create($"{AppContext.BaseDirectory}\\Files\\products2.xml");

        var diffOpts = XmlDiffOptions.IgnoreNamespaces | XmlDiffOptions.IgnoreComments;
        var diff = new XmlDiff.XmlDiffPatch.XmlDiff(diffOpts);

        // Act
        var isEqual = diff.Compare(xml1, xml2);

        // Assert
        Assert.IsFalse(isEqual);
    }
}