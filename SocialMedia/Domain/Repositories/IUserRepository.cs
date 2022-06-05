using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Repositories.Common;

namespace SocialMedia.Domain.Repositories;

public interface IUserRepository : IRepositoryAsync<User>
{
}