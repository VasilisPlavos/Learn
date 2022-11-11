using System.Net;

namespace TestEntries.Common.Contracts
{
    public class ResponseHeader
    {
        public List<Error>? Errors { get; set; }
        public bool ResultStatus { get; set; }
    }

    public class Error
    {
        public string? Component { get; set; }
        public string? Description { get; set; }
        public int ErrorCode { get; set; }
        public string? ErrorType { get; set; }
        public string? SeverityLevel { get; set; }
        public string? SourceSystem { get; set; }
        public string? Suggestion { get; set; }
        public string? TechnicalDescription { get; set; }
    }
}
