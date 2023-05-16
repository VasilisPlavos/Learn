using Microsoft.AspNetCore.Authentication.JwtBearer;
using PoC.Authentication.API.Services;

namespace PoC.Authentication.API.Helpers;

public class JwtBearerEventsHandler : JwtBearerEvents
{
	private readonly IAuthService _authService;

	public JwtBearerEventsHandler(IAuthService authService)
	{
		_authService = authService;
		OnMessageReceived = async context => { await MessageReceivedHandler(context); };
	}

	private async Task MessageReceivedHandler(MessageReceivedContext context)
	{

		await _authService.AuthAsync(context);
	}
}