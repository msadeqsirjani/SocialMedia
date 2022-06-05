namespace SocialMedia.Application.ViewModels;

public class JwtTokenDto
{
    public JwtTokenDto(Guid userId, string? username, string? token, int expiry)
    {
        UserId = userId;
        Username = username;
        Token = token;
        Expiry = expiry;
    }

    public Guid UserId { get; set; }
    public string? Username { get; set; }
    public string? Token { get; set; }
    public int Expiry { get; set; }
}