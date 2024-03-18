using System.Diagnostics;

namespace Contracts.Helpers;

public static class ConvertToAudioHelper
{
    public static async Task<String> ConvertStreamVideoToAudioStreamAndUploadOnS3(AppSettings appSettings, Stream stream)
    {
        try
        {
            // Create a temporary file in a cross-platform way
            string tempVideoFilePath = Path.GetTempFileName();
            Console.WriteLine("Temp path: " +tempVideoFilePath);

            try
            {
                // Write the stream to the temporary file
                using (FileStream tempVideoFileStream = File.OpenWrite(tempVideoFilePath))
                {
                    await stream.CopyToAsync(tempVideoFileStream);
                }

                // Convert the video to audio
                using (Stream audioStream = await ConvertVideoToAudio(tempVideoFilePath))
                {
                    // Upload the audio stream to S3
                    String link = await AWSHelper.UploadStreamToS3(appSettings, audioStream);
                    return link;
                }
            }
            finally
            {
                // Ensure cleanup of the temp file even if exceptions occur
                if (File.Exists(tempVideoFilePath))
                {
                    File.Delete(tempVideoFilePath);
                }
            }
        }
        catch (Exception ex)
        {
            // Log or handle any errors that occur during the process
            // Consider logging or throwing a custom exception
            Console.Error.WriteLine("Error during conversion and upload:", ex);
            throw; // Or throw a custom exception
        }
    }
    
    private static async Task<Stream> ConvertVideoToAudio(string inputFilePath)
    {
        // Create a memory stream to hold the converted audio
        var audioStream = new MemoryStream();

        // Execute FFmpeg command to convert the video file to audio and write to the memory stream
        var ffmpegProcess = new Process
        {
            StartInfo =
            {
                FileName = "ffmpeg", // Assumes ffmpeg is in the system PATH
                Arguments = $"-i \"{inputFilePath}\" -vn -acodec libmp3lame -b:a 64k -f mp3 pipe:1",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            }
        };

        ffmpegProcess.Start();
    
        // Read audio stream from FFmpeg's standard output
        await ffmpegProcess.StandardOutput.BaseStream.CopyToAsync(audioStream);
        ffmpegProcess.WaitForExit();

        audioStream.Position = 0; // Reset the position of the memory stream to the beginning

        return audioStream;
    }}