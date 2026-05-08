
namespace Application.Helpers;
public class FileHelper
{
    public static string GetFileExtension(string fileName)
    {
        if (string.IsNullOrEmpty(fileName))
            return string.Empty;

        int lastDotIndex = fileName.LastIndexOf('.');
        if (lastDotIndex < 0 || lastDotIndex == fileName.Length - 1)
            return string.Empty;

        return fileName.Substring(lastDotIndex + 1);
    }

    public static string FromFileToText(FileStream fileStream, string type)
    {
        if (fileStream == null || string.IsNullOrEmpty(type))
            return string.Empty;

        switch (type.ToLower())
        {
            case "txt":
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    return reader.ReadToEnd();
                }
            case "pdf":
                // Implement PDF reading logic here, e.g., using a library like iTextSharp
                return "PDF content extraction not implemented.";
            default:
                return "Unsupported file type.";
        }
    }
}