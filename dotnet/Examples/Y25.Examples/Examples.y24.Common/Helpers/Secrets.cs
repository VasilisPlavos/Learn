using System.Text.Json.Nodes;

namespace Examples.y24.Common.Helpers
{
    public static class Secrets
    {
        private static readonly string SecretFilePath = Path.Combine(AppContext.BaseDirectory, "Secrets.json");
        public static string Get(string keyValue)
        {
            try
            {
                var jsonContent = File.ReadAllText(SecretFilePath);
                var result = JsonNode.Parse(jsonContent)?[keyValue]?.ToString();
                return result ?? "";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return "";
        }
    }
}
