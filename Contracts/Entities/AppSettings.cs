namespace Contracts;

public class AppSettings
{
    public String DatabaseConnectionString { get; set; } = String.Empty;
    public AWSSettings? AwsSettings { get; set; }
    public string AdminToken { get; set; }
}