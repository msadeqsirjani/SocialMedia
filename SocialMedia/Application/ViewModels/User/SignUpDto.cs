namespace SocialMedia.Application.ViewModels.User;

public class SignUpDto
{
    [Required]
    [StringLength(64, MinimumLength = 4)]
    [RegularExpression(@"^(?i)(((?=.{6,21}$)[a-z\d]+\.[a-z\d]+)|[a-z\d]{5,20})$")]
    public string Username { get; set; } = null!;

    [Required]
    [StringLength(64, MinimumLength = 4)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required]
    [StringLength(64, MinimumLength = 4)]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string ConfirmationPassword { get; set; } = null!;
}