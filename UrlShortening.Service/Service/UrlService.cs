using System.Threading.Tasks;
using AutoMapper;
using UrlShortening.Domain.Model;
using UrlShortening.Repository.IRepository;
using UrlShortening.Service.IService;
using UrlShortening.Infrastructure.IInfrastructure;
using IConfiguration = UrlShortening.Infrastructure.IInfrastructure.IConfiguration;

namespace UrlShortening.Service.Service
{
    public class UrlService : IUrlService
    {
        private readonly IConversionService _conversionService;
        private readonly IUrlRepository _urlRepository;
        private readonly IConfiguration _configuration;

        public UrlService(IConversionService conversionService,
            IUrlRepository urlRepository,
            IConfiguration configuration)
        {
            _conversionService = conversionService;
            _urlRepository = urlRepository;
            _configuration = configuration;
        }
        /// <summary>
        /// Uses configuration class to get Hosting prefix from configuration file
        /// </summary>
        private string HostUrl
        {
            get { return _configuration.GetSetting(ConfigurationSetting.Parameter.HostingPrefix).ToString(); }
        }

        /// <summary>
        /// Either looks up an existing short url or creates a new one based on the actual url
        /// </summary>
        /// <param name="url">Actual Url</param>
        /// <returns>UrlModel in Response Object</returns>
        public async Task<Response> FindorCreateShortUrlAsync(string url)
        {
            var findExistingUrlResponse = await _urlRepository.RetrieveByUrlAsync(url);
            if (!findExistingUrlResponse.Succeeded)
            {
                return findExistingUrlResponse;
            }
            if (findExistingUrlResponse.Succeeded && findExistingUrlResponse.Model != null)
            {
                return ComposeShortUrlModel(findExistingUrlResponse);
            }
            
            return await SaveUrlAsync(url);
        }

        /// <summary>
        /// Saves Actual Url to database and composes short url
        /// </summary>
        /// <param name="url">Actual Url</param>
        /// <returns>UrlModel in Response Object</returns>
        private async Task<Response> SaveUrlAsync(string url)
        {
            var newUrl = Mapper.Map<UrlModel>(url);
            var createUrlResponse = await _urlRepository.CreateAsync(newUrl);
            if (!createUrlResponse.Succeeded)
            {
                return createUrlResponse;
            }
            return ComposeShortUrlModel(createUrlResponse);
        }

        /// <summary>
        /// Finds a long url corresponding to Key Value of shortUrl
        /// </summary>
        /// <param name="urlKey">Key Value of shortUrl</param>
        /// <returns>UrlModel in Response Object</returns>
        public async Task<Response> RetrieveLongUrlAsync(string urlKey)
        {
            var urlId = _conversionService.Base62StringToLong(urlKey);

            var findExistingUrlResponse = await _urlRepository.RetrieveAsync((int) urlId);
            if ((findExistingUrlResponse.Succeeded && findExistingUrlResponse.Model != null) || 
                !findExistingUrlResponse.Succeeded)
            {
                return findExistingUrlResponse;
            }

            findExistingUrlResponse.Succeeded = false;
            findExistingUrlResponse.Errors.Add("URL could not be found.");
            return findExistingUrlResponse;
        }

        /// <summary>
        /// Helper method to append create short url
        /// </summary>
        /// <param name="response">Response object with UrlModel</param>
        /// <returns>UrlModel in Response Object</returns>
        private Response ComposeShortUrlModel(Response response)
        {
            var updatedUrl = (UrlModel)response.Model;
            updatedUrl.ShortUrl = HostUrl + _conversionService.LongToBase62String(updatedUrl.Id);
            response.Model = updatedUrl;
            return response;
        }
    }
}