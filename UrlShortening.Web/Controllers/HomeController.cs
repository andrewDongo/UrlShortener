using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using UrlShortening.Domain.Model;
using UrlShortening.Service.IService;
using UrlShortening.Web.Models;

namespace UrlShortening.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUrlService _urlService;

        public HomeController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        public async Task<ActionResult> Index(string urlKey)
        {
            // browse to the home page (url key not provided)
            if (string.IsNullOrWhiteSpace(urlKey))
            {
                ViewBag.Title = "Andrew's Url Shortener";
                return View(new ShortenUrlViewModel());
            }

            // find shortened url based on url key
            var findShortenedUrlResponse = await _urlService.RetrieveLongUrlAsync(urlKey);

            // when url key doesn't match any recorded url
            if (!findShortenedUrlResponse.Succeeded)
            {
                ViewBag.Title = "404: URL Not Found";
                ViewBag.Message = "Sorry, requested URL could not be found";
                return View(new ShortenUrlViewModel());
            }

            var url = (UrlModel)findShortenedUrlResponse.Model;
            Response.StatusCode = (int)HttpStatusCode.Moved;
            Response.RedirectLocation = url.ActualUrl;
            return new ContentResult();
        }
    }
}
