namespace SocialMedia.Application.Services;


public interface IFileService
{
    Task<byte[]> DownloadAsync(string filename, CancellationToken cancellationToken = new());
    Task<string> UploadAsync(IFormFile file, CancellationToken cancellationToken = new());
}

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _environment;
    private const string UploadDirectory = "Uploads/";

    public FileService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public Task<byte[]> DownloadAsync(string filename, CancellationToken cancellationToken = new())
    {
        var path = Path.Combine(_environment.WebRootPath, UploadDirectory);

        var filePath = Path.Combine(path, filename);

        return File.ReadAllBytesAsync(filePath, cancellationToken);
    }

    public async Task<string> UploadAsync(IFormFile file, CancellationToken cancellationToken = new())
    {
        var path = Path.Combine(_environment.WebRootPath, UploadDirectory);

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(path, fileName);

        await using var stream = File.Create(filePath);
        await file.CopyToAsync(stream, cancellationToken);

        return fileName;
    }
}