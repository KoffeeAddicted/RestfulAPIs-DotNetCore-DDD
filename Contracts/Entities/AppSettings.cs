namespace Contracts;

public class AppSettings
{
    public String DatabaseConnectionString { get; set; } = String.Empty;
    public AWSSettings? AwsSettings { get; set; }
}