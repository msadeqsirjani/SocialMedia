using SocialMedia.Application.Services.Common;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Repositories.Common;

namespace SocialMedia.Application.Services;

public interface IPostService : IServiceAsync<Post>
{

}

public class PostService : ServiceAsync<Post>, IPostService
{
    public PostService(IRepositoryAsync<Post> repository) : base(repository)
    {
    }
}