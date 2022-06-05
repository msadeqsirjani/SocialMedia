namespace SocialMedia.Common.Common;

public static class ContentHelper
{
    public static string ToContentType(string filename)
    {
        var fileExtension = Path.GetExtension(filename).ToLower();

        return fileExtension switch
        {
            ".jpg" => "image/jpg",
            ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".bmp" => "image/bmp",
            _ => "application/octet-stream"
        };
    }
}