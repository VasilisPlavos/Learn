namespace TestEntries.Common.Contracts
{
    public class PackageResponse<TResponseContract>
    {
        public string ApprovalHeader { get; set; }
        public string EnvHeader { get; set; }
        public ResponseHeader ResponseHeader { get; set; }
        public string LogHeader { get; set; }
        public TResponseContract? ResponseData { get; set; }
    }
}
