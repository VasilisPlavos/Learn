using Examples.y24.Common.Dtos.Joke;
using System.IO;
using System.Xml.Linq;

namespace Examples.y24.DataStructure.Helpers;

public class XmlHelper
{
    public static async Task ExamplesAsync()
    {
        Example1();
        Example2();
        Example3();
        Example4();
        Example5();
    }

    private static void Example1()
    {
        string xml1 = @"<root><child1>text1</child1><child2 attr1='value1'>text2</child2></root>";
        string xml2 = @"<root><child1>text1</child1><child2 attr1='value1'>text2</child2></root>";

        var differences = CompareXml(xml1, xml2);
    }


    private static void Example2()
    {
        var xml1 = "<example>\r\n\r\n\r\n\r\n  <person>\r\n\r\n\r\n\r\n \r\n\r\n    <tel/>   <firstName/>\r\n\r\n<lastName/>\r\n\r\n\r\n\r\n  </person>\r\n\r\n\r\n\r\n</example>";
        var xml2 = "<example>\r\n  <person>\r\n    <firstName/>\r\n    <lastName/>\r\n\r\n  </person>\r\n</example>";
        var differences = CompareXml(xml1, xml2);
    }
    private static void Example3()
    {
        var xml1 = "<example>\r\n  <person>\r\n    <firstName/>\r\n    <lastName/>\r\n\r\n  </person>\r\n</example>";
        var xml2 = "<example>\r\n\r\n\r\n\r\n  <person>\r\n\r\n\r\n\r\n \r\n\r\n    <tel/>   <firstName/>\r\n\r\n<lastName/>\r\n\r\n\r\n\r\n  </person>\r\n\r\n\r\n\r\n</example>";
        var differences = CompareXml(xml1, xml2);
    }

    private static void Example4()
    {
        var xml1 = @"<root><child1>text1</child1><child2 attr1='value1'>text2</child2></root>";
        var xml2 = @"<root><child1>text1</child1><child2 attr2='value2'>text3</child2></root>";
        var differences = CompareXml(xml1, xml2, true);
    }

    public static List<string> CompareXml(string xml1, string xml2, bool ignoreNodeValues = false)
    {
        var doc1 = XDocument.Parse(xml1);
        var doc2 = XDocument.Parse(xml2);

        return CompareNodes(doc1.Root, doc2.Root, ignoreNodeValues);
    }

    private static void Example5()
    {
        var xml1 = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\Files\\products1.xml");
        var xml2 = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\Files\\products2.xml");

        var differences = CompareXml(xml1, xml2);
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
            var xNode2 = childNodes2.OfType<XElement>().FirstOrDefault(x => x.Name.LocalName == xNode1.Name.LocalName);
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
}