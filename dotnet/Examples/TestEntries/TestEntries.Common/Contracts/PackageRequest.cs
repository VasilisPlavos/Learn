namespace TestEntries.Common.Contracts
{
    public class PackageRequest<TRequestContract>
    {
        public string ApprovalHeader { get; set; }
        public string EnvHeader { get; set; }
        public string LogHeader { get; set; }
        public TRequestContract RequestData { get; set; }
    }
}
