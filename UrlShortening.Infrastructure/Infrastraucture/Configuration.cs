using System;
using System.Configuration;
using UrlShortening.Infrastructure.IInfrastructure;

namespace UrlShortening.Infrastructure.Infrastraucture
{
    public class Configuration : IConfiguration
    {
        private readonly ILogger _logger;

        public Configuration(ILogger logger)
        {
            _logger = logger;
        }

        public object GetSetting(ConfigurationSetting.Parameter parameter)
        {
            try
            {
                return ConfigurationManager.AppSettings.Get(parameter.ToString());
            }
            catch (Exception e)
            {
                _logger.LogError("Error getting setting: "+parameter,e);
                return null;
            }
            
        }
    }
}