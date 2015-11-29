namespace UrlShortening.Infrastructure.IInfrastructure
{
    public interface IConfiguration
    {
        object GetSetting(ConfigurationSetting.Parameter parameter);
    }

    public class ConfigurationSetting
    {
        // All relevant corresponding app settings keys
        public enum Parameter
        {
            HostingPrefix
        }
    }
}