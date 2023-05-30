using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace HelloWorldCode.Helpers.Middlewares;

public class AuthenticationMiddleware : IFunctionsWorkerMiddleware
{
    private readonly JwtSecurityTokenHandler _tokenValidator;
    private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly ConfigurationManager<OpenIdConnectConfiguration> _configurationManager;

    public AuthenticationMiddleware(IConfiguration configuration)
    {
        var authority = configuration["AuthenticationAuthority"];
        var audience = configuration["AuthenticationClientId"];
        _tokenValidator = new JwtSecurityTokenHandler();
        _tokenValidationParameters = new TokenValidationParameters
        {
            ValidAudience = audience
        };
        _configurationManager = new ConfigurationManager<OpenIdConnectConfiguration>(
            $"{authority}/.well-known/openid-configuration",
            new OpenIdConnectConfigurationRetriever());
    }


    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        if (!TokenHelper.TryGetFromHeaders(context, out var token))
        {
            // Unable to get token from headers
            context.SetHttpResponseStatusCode(HttpStatusCode.Unauthorized);
            return;
        }

        if (!_tokenValidator.CanReadToken(token))
        {
            // Token is malformed
            context.SetHttpResponseStatusCode(HttpStatusCode.Unauthorized);
            return;
        }

        // Get OpenID Connect metadata
        var validationParameters = _tokenValidationParameters.Clone();
        var openIdConfig = await _configurationManager.GetConfigurationAsync(default);
        validationParameters.ValidIssuer = openIdConfig.Issuer;
        validationParameters.IssuerSigningKeys = openIdConfig.SigningKeys;

        try
        {
            // Validate token
            var principal = _tokenValidator.ValidateToken(
                token, validationParameters, out _);

            // Set principal + token in Features collection
            // They can be accessed from here later in the call chain
            //context.Features.Set(new JwtPrincipalFeature(principal, token));

            await next(context);
        }
        catch (SecurityTokenException)
        {
            // Token is not valid (expired etc.)
            context.SetHttpResponseStatusCode(HttpStatusCode.Unauthorized);
            return;
        }



        Console.WriteLine("AuthMw: Do stuff before function");
        await next(context);
        Console.WriteLine("AuthMw: Do stuff after function");
    }
}