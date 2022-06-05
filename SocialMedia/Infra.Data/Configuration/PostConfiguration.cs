using SocialMedia.Domain.Entities;
using SocialMedia.Infra.Data.Configuration.Common;

namespace SocialMedia.Infra.Data.Configuration;

public class PostConfiguration : BaseEntityConfiguration<Post>
{
    public override void Configure(EntityTypeBuilder<Post> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Content).IsRequired().HasMaxLength(4096);
        builder.Property(x => x.IsImage).IsRequired();
        builder.Property(x => x.UserId).IsRequired();

        builder.HasOne(x => x.User)
            .WithMany(x => x.Posts)
            .HasForeignKey(x => x.UserId);
    }
}