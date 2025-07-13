using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Y25.AWS.Lambda.HelloWorld;

public class HelloWorldLambdaFunction
{
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }


    // Example JSON input for the Lambda function handler:
    //{
    //    "Name": "John",
    //    "Age": 30
    //}

    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="person">The event for the Lambda function handler to process.</param>
    /// <param name="context">The ILambdaContext that provides methods for logging and describing the Lambda environment.</param>
    /// <returns></returns>
    public Person FunctionHandler(Person person, ILambdaContext context)
    {
        person.Name = $"Who {person.Name}?";
        return person;
    }
}
