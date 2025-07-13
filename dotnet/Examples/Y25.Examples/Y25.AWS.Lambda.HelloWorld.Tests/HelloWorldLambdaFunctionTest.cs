using Xunit;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;

namespace Y25.AWS.Lambda.HelloWorld.Tests;

public class HelloWorldLambdaFunctionTest
{
    [Fact]
    public void TestToUpperFunction()
    {

        // Invoke the lambda function and confirm the string was upper cased.
        var function = new HelloWorldLambdaFunction();
        var context = new TestLambdaContext();
        var upperCase = function.FunctionHandler(new HelloWorldLambdaFunction.Person(){ Age = 2, Name = "Peter"}, context);

        Assert.NotNull(upperCase);
    }
}
