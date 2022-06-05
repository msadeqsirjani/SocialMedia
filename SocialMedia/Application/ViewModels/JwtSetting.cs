namespace SocialMedia.Application.ViewModels;

public class JwtSetting
{
    public bool ValidateIssuerSigningKey { get; set; }
    public string IssuerSigningKey { get; set; } = null!;
    public bool ValidateIssuer { get; set; } = true;
    public string? ValidIssuer  { get; set; }
    public bool ValidateAudience { get; set; } = true;
    public string? ValidAudience  { get; set; }
    public bool RequireExpirationTime  { get; set; }
    public bool ValidateLifetime { get; set; } = true;
}