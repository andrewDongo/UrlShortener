using System.Threading.Tasks;
using UrlShortening.Domain.Model;

namespace UrlShortening.Service.IService
{
    public interface IUrlService
    {
        Task<Response> FindorCreateShortUrlAsync(string url);
        Task<Response> RetrieveLongUrlAsync(string urlKey);
    }

}