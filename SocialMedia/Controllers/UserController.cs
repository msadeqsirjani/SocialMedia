using SocialMedia.Application.Services;
using SocialMedia.Application.ViewModels.User;

namespace SocialMedia.Controllers;

public class UserController : ApplicationController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(SignUpDto param)
    {
        var result = await _userService.SignUpAsync(param);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> SignIn(SignInDto param)
    {
        var result = await _userService.SignInAsync(param);

        return Ok(result);
    }
}