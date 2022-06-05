namespace SocialMedia.Application.ViewModels.Post;

public class PublishDto
{
    [Required]
    [StringLength(4096, MinimumLength = 4)]
    public string Content { get; set; } = null!;

    [Required]
    public bool IsImage { get; set; }

    public Guid UserId { get; set; }
}