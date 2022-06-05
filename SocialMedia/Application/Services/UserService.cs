using SocialMedia.Application.Services.Common;
using SocialMedia.Application.ViewModels.User;
using SocialMedia.Common.Common;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Repositories.Common;

namespace SocialMedia.Application.Services;

public interface IUserService : IServiceAsync<User>
{
    Task<Result> SignUpAsync(SignUpDto param, CancellationToken cancellationToken = new());
    Task<Result> SignInAsync(SignInDto param, CancellationToken cancellationToken = new());
}

public class UserService : ServiceAsync<User>, IUserService
{
    private readonly IJwtService _jwtService;

    public UserService(IRepositoryAsync<User> repository,
        IUnitOfWorkAsync unitOfWork, 
        IJwtService jwtService) : base(repository, unitOfWork)
    {
        _jwtService = jwtService;
    }

    public async Task<Result> SignUpAsync(SignUpDto param, CancellationToken cancellationToken = new())
    {
        if(await Repository.ExistsAsync(x=>x.Username == param.Username, cancellationToken))
            return Result.WithMessage("Username is exist. Please SignInAsync");

        User user = new()
        {
            Id = Guid.NewGuid(),
            Username = param.Username,
            Password = Security.Encrypt(param.Password),
            PasswordConfirmed = param.Password.Equals(param.ConfirmationPassword)
        };

        await Repository.AddAsync(user, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);

        var token = await _jwtService.GenerateJwtTokenAsync(user.Id, param.Username);

        return Result.WithSuccess(token);
    }

    public async Task<Result> SignInAsync(SignInDto param, CancellationToken cancellationToken = new())
    {
        var user = await Repository.FirstOrDefaultAsync(x => x.Username == param.Username, cancellationToken);

        if (user is null)
            return Result.WithMessage("Username doesn't exist");

        if(user.Password != Security.Encrypt(param.Password))
            return Result.WithMessage("Username or Password is wrong");

        var token = await _jwtService.GenerateJwtTokenAsync(user.Id, user.Username);
            
        return Result.WithSuccess(token);
    }
}