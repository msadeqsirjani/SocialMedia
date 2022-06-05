using SocialMedia.Application.Services;
using SocialMedia.Common.Common;

namespace SocialMedia.Controllers;

[Authorize]
public class FileController : ApplicationController
{
    private readonly IFileService _fileService;

    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }

    [HttpGet]
    public async Task<IActionResult> Download(string filename)
    {
        var content = await _fileService.DownloadAsync(filename);

        return File(content, ContentHelper.ToContentType(filename));
    }

    [HttpPost]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        var filename = await _fileService.UploadAsync(file);

        var downloadLink = (Request.IsHttps ? "https" : "http") + $"://{Request.Host}/File/Download?filename={filename}";

        return Ok(Result.WithSuccess(new
        {
            Filename = filename,
            DownloadLink = downloadLink
        }));
    }
}