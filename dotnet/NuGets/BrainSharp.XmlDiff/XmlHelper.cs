﻿using System.Xml.Linq;

namespace BrainSharp.XmlDiff;

public static class XmlHelper
{
    public static List<string> CompareXml(string xml1, string xml2, bool ignoreNodeValues = false)
    {
        var doc1 = XDocument.Parse(xml1);
        var doc2 = XDocument.Parse(xml2);

        return CompareNodes(doc1.Root, doc2.Root, ignoreNodeValues);
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

    private static XElement FindNode(List<XNode> childNodes2, XElement xNode1)
	{
		var xElement = childNodes2.OfType<XElement>().FirstOrDefault(x => x.ToString() == xNode1.ToString());
		xElement ??= childNodes2.OfType<XElement>().FirstOrDefault(x => x.Name.LocalName == xNode1.Name.LocalName);
		return xElement;
	}
}