namespace SocialMedia.Application.ViewModels.Post;

public class ShowPostDto
{
    public Guid Id { get; set; }
    public string Content { get; set; } = null!;
    public bool IsImage { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public string Publisher { get; set; } = null!;
}