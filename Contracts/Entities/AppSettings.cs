namespace Contracts;

public class AppSettings
{
    public String DatabaseConnectionString { get; } = String.Empty;
    public AWSSettings? AwsSettings { get; set; }
}