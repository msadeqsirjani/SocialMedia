using SocialMedia.Domain.Common;

namespace SocialMedia.Domain.Entities;

public class User : BaseEntity
{
    public User()
    {
        Posts = new List<Post>();
    }

    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public bool PasswordConfirmed { get; set; }

    #region Relations

    public ICollection<Post> Posts { get; set; }

    #endregion
}