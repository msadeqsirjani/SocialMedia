using SocialMedia.Application.Services;
using SocialMedia.Application.ViewModels.Post;

namespace SocialMedia.Controllers;

[Authorize]
public class PostController : ApplicationController
{
    private readonly IPostService _postService;

    public PostController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpPost]
    public async Task<IActionResult> Publish(PublishDto param)
    {
        param.UserId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value!);

        var result = await _postService.PublishAsync(param);

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Modify(ModifyDto param)
    {
        param.UserId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value!);

        var result = await _postService.ModifyAsync(param);

        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> Remove(Guid id)
    {
        var userId = Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value!);

        var result = await _postService.RemoveAsync(id, userId);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> ShowPosts()
    {
        var result = await _postService.ShowPostsAsync();

        return Ok(result);
    }
}