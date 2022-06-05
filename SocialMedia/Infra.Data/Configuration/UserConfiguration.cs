using SocialMedia.Domain.Entities;
using SocialMedia.Infra.Data.Configuration.Common;

namespace SocialMedia.Infra.Data.Configuration;

public class UserConfiguration : BaseEntityConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.Property(x => x.Username).IsRequired().HasMaxLength(64);
        builder.Property(x => x.Password).IsRequired();
        builder.Property(x => x.PasswordConfirmed).IsRequired();

        builder.HasMany(x => x.Posts)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId);


        builder.HasIndex(x => x.Username, "IX_Users_Username").IsUnique();
    }
}