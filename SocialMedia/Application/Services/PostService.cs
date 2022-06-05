using SocialMedia.Application.Services.Common;
using SocialMedia.Application.ViewModels.Post;
using SocialMedia.Common.Common;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Repositories.Common;

namespace SocialMedia.Application.Services;

public interface IPostService : IServiceAsync<Post>
{
    Task<Result> PublishAsync(PublishDto param, CancellationToken cancellationToken = new());
    Task<Result> ShowPostsAsync(CancellationToken cancellationToken = new());
    Task<Result> RemoveAsync(Guid id, Guid userId, CancellationToken cancellationToken = new());
    Task<Result> ModifyAsync(ModifyDto param, CancellationToken cancellationToken = new());
}

public class PostService : ServiceAsync<Post>, IPostService
{
    public PostService(IRepositoryAsync<Post> repository, IUnitOfWorkAsync unitOfWork) : base(repository, unitOfWork)
    {
    }

    public async Task<Result> PublishAsync(PublishDto param, CancellationToken cancellationToken = new())
    {
        Post post = new()
        {
            Id = Guid.NewGuid(),
            Content = param.Content,
            IsImage = param.IsImage,
            UserId = param.UserId
        };

        await Repository.AddAsync(post, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.WithSuccess(Statement.Success);
    }

    public async Task<Result> ShowPostsAsync(CancellationToken cancellationToken = new())
    {
        var posts = await Repository.Queryable(false)
            .Include(x => x.User)
            .OrderByDescending(x => x.ModifiedAt)
            .ThenByDescending(x => x.CreatedAt)
            .Select(x => new ShowPostDto
            {
                Id = x.Id,
                Content = x.Content,
                IsImage = x.IsImage,
                CreatedAt = x.CreatedAt,
                ModifiedAt = x.ModifiedAt,
                Publisher = x.User.Username
            }).ToListAsync(cancellationToken);

        return Result.WithSuccess(posts);
    }

    public async Task<Result> RemoveAsync(Guid id, Guid userId, CancellationToken cancellationToken = new())
    {
        var post = await Repository.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (post == null)
            return Result.WithMessage("Post not found");

        if (post.UserId != userId)
            return Result.WithException("This post doesn't belong to you");

        await Repository.DeleteAsync(id, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.WithSuccess(Statement.Success);
    }

    public async Task<Result> ModifyAsync(ModifyDto param, CancellationToken cancellationToken = new())
    {
        var post = await Repository.FirstOrDefaultAsync(x => x.Id == param.Id, cancellationToken);

        if (post == null)
            return Result.WithMessage("Post not found");

        if (post.UserId != param.UserId)
            return Result.WithException("This post doesn't belong to you");

        post.Content = param.Content;
        post.IsImage = param.IsImage;

        Repository.Update(post);
        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.WithSuccess(Statement.Success);
    }
}