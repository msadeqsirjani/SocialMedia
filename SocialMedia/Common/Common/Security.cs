namespace SocialMedia.Common.Common;


public static class Security
{
    private const string PlainText = "C0915809-B937-4E84-B7BA-97EFCF9AF77C";

    public static string Encrypt(string text)
    { 
        var clearBytes = Encoding.Unicode.GetBytes(text);
        using var aes = Aes.Create();
        var pdb = new Rfc2898DeriveBytes(PlainText,
            new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        aes.Key = pdb.GetBytes(32);
        aes.IV = pdb.GetBytes(16);
        using var stream = new MemoryStream();
        using (var cs = new CryptoStream(stream, aes.CreateEncryptor(), CryptoStreamMode.Write))
        {
            cs.Write(clearBytes, 0, clearBytes.Length);
            cs.Close();
        }

        text = Convert.ToBase64String(stream.ToArray());

        return text;
    }

    public static string Decrypt(string text)
    {
        text = text.Replace(" ", "+");
        var cipherBytes = Convert.FromBase64String(text);
        using var aes = Aes.Create();
        var pdb = new Rfc2898DeriveBytes(PlainText,
            new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
        aes.Key = pdb.GetBytes(32);
        aes.IV = pdb.GetBytes(16);
        using var ms = new MemoryStream();
        using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
        {
            cs.Write(cipherBytes, 0, cipherBytes.Length);
            cs.Close();
        }

        text = Encoding.Unicode.GetString(ms.ToArray());

        return text;
    }
}