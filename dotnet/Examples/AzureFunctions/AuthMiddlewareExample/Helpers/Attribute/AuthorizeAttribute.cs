using System;

namespace HelloWorldCode.Helpers.Attribute;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : System.Attribute
{
    /// <summary>
    /// Defines which scopes (aka delegated permissions)
    /// are accepted. In this sample these
    /// must be combined with <see cref="UserRoles"/>.
    /// </summary>
    public string[] Scopes { get; set; } = Array.Empty<string>();
    /// <summary>
    /// Defines which user roles are accepted.
    /// Must be combined with <see cref="Scopes"/>.
    /// </summary>
    public string[] UserRoles { get; set; } = Array.Empty<string>();
    /// <summary>
    /// Defines which app roles (aka application permissions)
    /// are accepted.
    /// </summary>
    public string[] AppRoles { get; set; } = Array.Empty<string>();
}