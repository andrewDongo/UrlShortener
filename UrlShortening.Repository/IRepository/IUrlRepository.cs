using System.Threading.Tasks;
using UrlShortening.Domain.Model;

namespace UrlShortening.Repository.IRepository
{
    public interface IUrlRepository
    {
        Task<Response> CreateAsync(UrlModel item);
        Task<Response> RetrieveAsync(int id);
        Task<Response> RetrieveByUrlAsync(string url);
    }
}