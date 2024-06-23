using System.Xml.Linq;

namespace BrainSharp.Xml;

public class Xml
{
    public static List<string> ExplainDifference(XDocument doc1, XDocument doc2, bool ignoreNodeValues = false)
    {
        return CompareNodes(doc1.Root, doc2.Root, ignoreNodeValues);
    }

    public static List<string> ExplainDifference(string xml1Content, string xml2Content, bool ignoreNodeValues = false)
    {
        var doc1 = Parse(xml1Content);
        var doc2 = Parse(xml2Content);
        return CompareNodes(doc1.Root, doc2.Root, ignoreNodeValues);
    }

    public static List<string> ExplainDifferenceFromFiles(string file1Path, string file2Path, bool ignoreNodeValues = false)
    {
        var doc1 = ParseFromFile(file1Path);
        var doc2 = ParseFromFile(file2Path);
        return CompareNodes(doc1.Root, doc2.Root, ignoreNodeValues);
    }

    public static async Task<List<string>> ExplainDifferenceFromFilesAsync(string file1Path, string file2Path, bool ignoreNodeValues = false)
    {
        var doc1 = await ParseFromFileAsync(file1Path);
        var doc2 = await ParseFromFileAsync(file2Path);
        return CompareNodes(doc1.Root, doc2.Root, ignoreNodeValues);
    }

    public static bool IsEqual(XDocument doc1, XDocument doc2, bool ignoreNodeValues = false)
    {
        var differences = CompareNodes(doc1.Root, doc2.Root, ignoreNodeValues);
        return differences.Count == 0;
    }

    /// <summary>
    /// Create a new <see cref="XDocument"/> from a string containing XML.
    /// </summary>
    /// <param name="text">A string containing XML.</param>
    /// <returns>
    /// An <see cref="XDocument"/> containing an XML tree initialized from the passed in XML string.
    /// </returns>
    public static XDocument Parse(string text)
    {
        return XDocument.Parse(text);
    }

    public static XDocument ParseFromFile(string path)
    {
        return XDocument.Load(path);
    }

    // TODO: Not working as explected - currently override by sync
    public static async Task<XDocument> ParseFromFileAsync(string path)
    {
        return ParseFromFile(path);
    //    var stream = XmlReader.Create(path, new XmlReaderSettings { Async = true });
    //    return await XDocument.LoadAsync(stream, loadOptions, cancellationToken);
    }

    private static List<string> CompareNodes(XElement node1, XElement node2, bool ignoreNodeValues = false)
    {
        var differences = new List<string>();

        if (node1.Name != node2.Name)
        {
            differences.Add($"Tags differ: {node1.Name} vs {node2.Name}");
            return differences;
        }

        var childNodes1 = node1.Nodes().ToList();
        var childNodes2 = node2.Nodes().ToList();

        if (!ignoreNodeValues && childNodes1.Count == 1)
        {
            if (node1.Value.Trim() != node2.Value.Trim())
            {
                differences.Add($"Texts differ on node {node1.Name}: {node1.Value} vs {node2.Value}");
            }
        }

        // Compare attributes
        var node1Attributes = node1.Attributes().ToDictionary(a => a.Name, a => a.Value);
        var node2Attributes = node2.Attributes().ToDictionary(a => a.Name, a => a.Value);

        foreach (var attr1 in node1Attributes)
        {
            var attr2 = node2Attributes.FirstOrDefault(x => x.Key == attr1.Key);
            if (attr2.Key == null)
            {
                differences.Add($"Missing attribute on second xml {attr1.Key}");
                continue;
            }

            if (attr1.Value != attr2.Value)
            {
                differences.Add($"Attributes differ on key {attr1.Key} with values {attr1.Value} vs {attr2.Value}");
            }
        }

        foreach (var attr2 in node2Attributes)
        {
            var exist = node1Attributes.Any(x => x.Key == attr2.Key);
            if (!exist)
            {
                differences.Add($"Missing attribute on first xml {attr2.Key}");
            }
        }

        // Compare child nodes recursively
        foreach (var xNode1 in childNodes1.OfType<XElement>())
        {
            var xNode2 = FindNode(childNodes2, xNode1);
            if (xNode2 == null)
            {
                differences.Add($"Missing child node on second xml: {xNode1.Name}");
                continue;
            }

            childNodes2.Remove(xNode2);
            var childNodesDifferences = CompareNodes(xNode1, xNode2, ignoreNodeValues);
            differences.AddRange(childNodesDifferences);
        }

        foreach (var xNode2 in childNodes2.OfType<XElement>())
        {
            var exist = childNodes1.OfType<XElement>().Any(x => x.Name.LocalName == xNode2.Name.LocalName);
            if (!exist)
            {
                differences.Add($"Missing child node on first xml: {xNode2.Name}");
            }
        }

        return differences;
    }

    static XElement FindNode(List<XNode> childNodes2, XElement xNode1)
    {
        var xElement = childNodes2.OfType<XElement>().FirstOrDefault(x => x.ToString() == xNode1.ToString());
        xElement ??= childNodes2.OfType<XElement>().FirstOrDefault(x => x.Name.LocalName == xNode1.Name.LocalName);
        return xElement;
    }

}
