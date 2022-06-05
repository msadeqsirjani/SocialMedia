using SocialMedia.Application.Services.Common;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Repositories.Common;

namespace SocialMedia.Application.Services;

public interface IUserService : IServiceAsync<User>
{

}

public class UserService : ServiceAsync<User>, IUserService
{
    public UserService(IRepositoryAsync<User> repository) : base(repository)
    {
    }
}