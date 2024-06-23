[![NuGet](https://img.shields.io/nuget/v/BrainSharp.Xml.svg)](https://www.nuget.org/packages/BrainSharp.Xml/)

# .NET Core library to handle XML files

Handle and compare xml file easily. Please see the examples:

## Installation Instructions
Nuget package available (https://www.nuget.org/packages/BrainSharp.Xml/)
```
Install-Package BrainSharp.Xml
```
dotnet cli:
```
dotnet add package BrainSharp.Xml
```

## Parse Xml from String content
```
using BrainSharp.Xml;

var xml1 = @"<root><child1>text1</child1><child2 attr1='value1'>text2</child2></root>";
var xDocument = Xml.Parse(xml1);
```

## Parse Xml from file asynchronously
```
using BrainSharp.Xml;

var filePath = $"{AppContext.BaseDirectory}\\Files\\products1.xml";
var xDocument = await Xml.ParseFromFileAsync(filePath);
```

## Find the differences from the XML files
```
using BrainSharp.Xml;

string xml1 = @"<root><child1>text1</child1><child2 attr1='value1'>text2</child2></root>";
string xml2 = @"<root><child1>text1</child1><child2 attr1='value1'>text2</child2></root>";

listOfDifferences = Xml.ExplainDifference(xml1, xml2);
foreach (var difference in listOfDifferences)
{
    Console.WriteLine(difference); // This will not print nothing, since there are no differences
}  
```

## Find the differences from XML files, asynchronously using the Filepath
```
using BrainSharp.Xml;

var filePath1 = $"{AppContext.BaseDirectory}\\Files\\products1.xml";
var filePath2 = $"{AppContext.BaseDirectory}\\Files\\products2.xml";
var listOfDifferences = await Xml.ExplainDifferenceFromFilesAsync(filePath1, filePath2);
foreach (var difference in listOfDifferences)
{
    Console.WriteLine(difference);
}
```

## Optional parameters
1. Avoid... [Add context]

## Roadmap
* Nothing at the moment

## More about this library
[Add context]

## MIT License

Copyright (c) 2024 Vasilis Plavos

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
