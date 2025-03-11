using System.Xml.Linq;
using BrainSharp.Xml;

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

        return Xml.ExplainDifference(doc1, doc2);
    }

    private static void Example5()
    {
        var xml1 = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\Files\\products1.xml");
        var xml2 = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\Files\\products2.xml");

        var differences = CompareXml(xml1, xml2);
    }


}