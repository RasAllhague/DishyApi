namespace DishyApi.Configuration;

/// <summary>
/// Contains configuration values for the jwt setup.
/// </summary>
public class JwtSettings
{
    /// <summary>
    /// The issuer of the token.
    /// </summary>
    public string Issuer { get; set; }
    /// <summary>
    /// The audience of the token.
    /// </summary>
    public string Audience { get; set; }
    /// <summary>
    /// The private key used for the token.
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// Creates a <see cref="JwtSettings"/> instance with all properties set to <see cref="string.Empty"/>.
    /// </summary>
    public JwtSettings()
    {
        Issuer = string.Empty;
        Audience = string.Empty;
        Key = string.Empty;
    }
}
