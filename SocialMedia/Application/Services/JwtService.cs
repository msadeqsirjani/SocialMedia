using SocialMedia.Application.ViewModels;

namespace SocialMedia.Application.Services;


public interface IJwtService
{
    Task<JwtTokenDto> GenerateJwtToken(Guid userId, string username, bool administrator);
}

public class JwtService : IJwtService
{
    public const string Administrator = nameof(Administrator);
    public const string Other = nameof(Other);

    private readonly JwtSetting _options;

    public JwtService(IOptions<JwtSetting> options)
    {
        _options = options.Value;
    }

    public Task<JwtTokenDto> GenerateJwtToken(Guid userId, string username, bool administrator)
    {
        var expires = DateTime.UtcNow.AddDays(1);
        var expiry = expires.ToEpochTimeSpan().TotalSeconds.ToInt32();
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, username),
            new(ClaimTypes.NameIdentifier, userId.ToString()),
            new(ClaimTypes.Role, administrator ? Administrator : Other),
            new(ClaimTypes.Expiration, expiry.ToString(CultureInfo.InvariantCulture))
        };
        var secretKey = Encoding.UTF8.GetBytes(_options.IssuerSigningKey);
        var signingCredentials =
            new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256);

        var jwtToken = new JwtSecurityToken(_options.ValidIssuer, _options.ValidAudience, claims, expires: expires, signingCredentials: signingCredentials);

        var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        var jwtTokenDto = new JwtTokenDto(userId, username, token, expiry);

        return Task.FromResult(jwtTokenDto);
    }
}