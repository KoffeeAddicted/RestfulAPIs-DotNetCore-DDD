using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Contracts.Helpers;

public static class AWSHelper
{
    private static IConfigurationRoot _configuration;

    static AWSHelper()
    {
        // No Int64er initializing configuration here
    }

    public static void Initialize(IConfiguration configuration)
    {
        _configuration = (IConfigurationRoot)configuration;
    }
    
    private static string GetSetting(string key)
    {
        if (_configuration != null)
        {
            return _configuration[key];

        }

        return null;
    }

    public static string Test()
    {
        var helo = GetSetting("AppSettings:AWS:AccessKey");
        return helo;
    }
}