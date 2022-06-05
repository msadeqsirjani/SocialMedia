using Microsoft.EntityFrameworkCore.Query.Internal;
using SocialMedia.Domain.Common;

namespace SocialMedia.Domain.Entities;

public class Post : BaseEntity
{
    public string Content { get; set; } = null!;
    public bool IsImage { get; set; }
    public Guid UserId { get; set; }

    #region Relations

    public User User { get; set; } = null!;

    #endregion
}