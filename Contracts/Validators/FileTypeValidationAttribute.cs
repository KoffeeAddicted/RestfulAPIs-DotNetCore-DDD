using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using Services.DbEnum;
using Services.DTOs.Episodes;

public class FileTypeValidationAttribute : ValidationAttribute
{
    public FileTypeValidationAttribute()
    {
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        try
        {
            String file = value as String;

            EpisodeCreateRequest episodeCreateRequest = (EpisodeCreateRequest)validationContext.ObjectInstance;

            if (String.IsNullOrEmpty(file))
            {
                // Property is not required, so it can be null
                return new ValidationResult("File is required");
            }

            switch (episodeCreateRequest.InputAudioType)
            {
                /*case InputUploadAudioTypeEnum.Base64File:
                    // Validate if the file is base64 encoded (omitted for simplicity)
                    if (!IsBase64String(file))
                    {
                        return new ValidationResult("Invalid base64 file format.");
                    }

                    break;*/
                case InputUploadAudioTypeEnum.YoutubeLink:
                    // Check if the link resembles a YouTube video URL
                    if (!IsYouTubeLink(file))
                    {
                        return new ValidationResult("Invalid YouTube link format.");
                    }

                    break;
                case InputUploadAudioTypeEnum.AudioLink:
                    // Check if the link is a valid URI and ends with a common audio extension
                    if (!IsUriAndEndsWithExtension(file, ".mp3", ".wav", ".ogg"))
                    {
                        return new ValidationResult("Invalid audio link format or extension.");
                    }

                    break;
                case InputUploadAudioTypeEnum.VideoLink:
                    // Check if the link is a valid URI and ends with a common video extension
                    if (!IsUriAndEndsWithExtension(file, ".mp4", ".avi", ".mkv"))
                    {
                        return new ValidationResult("Invalid video link format or extension.");
                    }

                    break;
                default:
                    return new ValidationResult("Invalid audio type.");
            }

            return ValidationResult.Success;
        }
        catch (Exception exception)
        {
            return new ValidationResult(exception.Message);
        }
    }

    private bool IsYouTubeLink(string url)
    {
        var uri = new Uri(url);

        if (uri.Host.ToLowerInvariant() != "www.youtube.com" && uri.Host.ToLowerInvariant() != "youtu.be")
            return false;

        return true;
    }

    private bool IsUriAndEndsWithExtension(string url, params string[] extensions)
    {
        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
            return false;

        var path = uri.LocalPath;
        foreach (var extension in extensions)
        {
            if (path.EndsWith(extension, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }
    
    private bool IsBase64String(string value)
    {
        // Check if the string is a valid base64 encoded string
        try
        {
            byte[] bytes = Convert.FromBase64String(value);
        
            // Extract the file extension from the base64-decoded bytes
            string extension = GetFileExtension(bytes);

            if (IsVideoExtensionValid(extension) || IsAudioExtensionValid(extension))
                return true;
            return false;
        }
        catch (FormatException)
        {
            return false; // Not a valid base64 string
        }
    }
    
    private string GetFileExtension(byte[] bytes)
    {
        // Sample implementation to extract file extension from the bytes
        // You might need to adjust this based on your specific requirements
        // This implementation assumes that the bytes contain a valid file header
        // that includes information about the file format

        // For simplicity, let's assume the file format is represented by the first few bytes
        // and the extension is determined based on the file format
        // You might need to use a more robust method depending on your use case

        // Here, we're assuming that the first few bytes represent the file format
        // and we're using a dictionary to map file formats to their corresponding extensions
        Dictionary<string, string> formatToExtensionMap = new Dictionary<string, string>
        {
            { "mp3", ".mp3" },
            { "wav", ".wav" },
            { "ogg", ".ogg" },
            { "mp4", ".mp4" },
            { "avi", ".avi" },
            { "mkv", ".mkv" }
            // Add more formats and extensions as needed
        };

        // Sample implementation to extract the file format
        string fileFormat = Encoding.ASCII.GetString(bytes.Take(4).ToArray()).ToLowerInvariant();

        // Sample implementation to map file format to extension
        if (formatToExtensionMap.TryGetValue(fileFormat, out string extension))
        {
            return extension;
        }

        return string.Empty; // Unable to determine extension
    }

    private bool IsAudioExtensionValid(string extension)
    {
        // Validate audio extensions
        // Customize this method based on the audio extensions you support
        return extension.Equals(".mp3", StringComparison.OrdinalIgnoreCase) ||
               extension.Equals(".wav", StringComparison.OrdinalIgnoreCase) ||
               extension.Equals(".ogg", StringComparison.OrdinalIgnoreCase);
    }

    private bool IsVideoExtensionValid(string extension)
    {
        // Validate video extensions
        // Customize this method based on the video extensions you support
        return extension.Equals(".mp4", StringComparison.OrdinalIgnoreCase) ||
               extension.Equals(".avi", StringComparison.OrdinalIgnoreCase) ||
               extension.Equals(".mkv", StringComparison.OrdinalIgnoreCase);
    }
}
