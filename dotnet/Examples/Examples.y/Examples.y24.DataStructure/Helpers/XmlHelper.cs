﻿using Examples.y24.Common.Dtos.Joke;
using System.Xml.Linq;

namespace Examples.y24.DataStructure.Helpers;

public class XmlHelper
{
    public static async Task ExamplesAsync()
    {
        Example1();
        Example2();
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
        string xml1 = @"<root><child1>text1</child1><child2 attr1='value1'>text2</child2></root>";
        string xml2 = @"<root><child1>text1</child1><child2 attr2='value2'>text3</child2></root>";

        var differences = CompareXml(xml1, xml2);
    }

    public static List<string> CompareXml(string xml1, string xml2)
    {
        XDocument doc1 = XDocument.Parse(xml1);
        XDocument doc2 = XDocument.Parse(xml2);

        return CompareNodes(doc1.Root, doc2.Root);
    }

    private static List<string> CompareNodes(XElement node1, XElement node2, bool compareNodeValues = true)
    {
        var differences = new List<string>();
        if (node1 == null && node2 == null) return differences;
        
        if (node1.Name != node2.Name)
        {
            differences.Add($"Tags differ: {node1.Name} vs {node2.Name}");
            return differences;
        }

        if (compareNodeValues)
        {
            if (node1.Value.Trim() != node2.Value.Trim())
            {
                differences.Add($"Texts differ: {node1.Value} vs {node2.Value}");
            }
        }

        // Compare attributes
        var attrs1 = node1.Attributes().ToDictionary(a => a.Name, a => a.Value);
        var attrs2 = node2.Attributes().ToDictionary(a => a.Name, a => a.Value);

        foreach (var attr in attrs1)
        {
            if (!attrs2.ContainsKey(attr.Key) || attrs2[attr.Key] != attr.Value)
            {
                differences.Add($"Attributes differ: {attr.Key} - {attr.Value} vs {attrs2.GetValueOrDefault(attr.Key)}");
            }
        }

        // Compare child nodes recursively
        var childNodes1 = node1.Nodes().ToList();
        var childNodes2 = node2.Nodes().ToList();

        foreach (var xNode1 in childNodes1.OfType<XElement>())
        {
            var xNode2 = childNodes2.OfType<XElement>().FirstOrDefault(x => x.Name.LocalName == xNode1.Name.LocalName);
            if (xNode2 == null)
            {
                differences.Add($"Missing child node on second xml: {xNode1.Name}");
                continue;
            }

            var childNodesDifferences = CompareNodes(xNode1, xNode2);
            differences.AddRange(childNodesDifferences);
        }

        foreach (var xNode2 in childNodes2.OfType<XElement>())
        {
            var exist = childNodes1.OfType<XElement>().Any(x => x.Name.LocalName == xNode2.Name.LocalName);
            if (!exist) differences.Add($"Missing child node on first xml: {xNode2.Name}");
        }

        return differences;
    }


}