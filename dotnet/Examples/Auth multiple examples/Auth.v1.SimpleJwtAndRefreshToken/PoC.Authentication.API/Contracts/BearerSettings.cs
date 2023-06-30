namespace PoC.Authentication.API.Contracts
{
	public class BearerSettings
	{
		public string Secret { get; set; }
		public List<string> ValidIssuers { get; set; }
		public List<string> ValidAudiences { get; set; }
	}
}
