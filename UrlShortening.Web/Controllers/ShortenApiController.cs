using System.Threading.Tasks;
using System.Web.Http;
using UrlShortening.Service.IService;
using UrlShortening.Web.Models;

namespace UrlShortening.Web.Controllers
{
    [RoutePrefix("api/shorten")]
    [Route("{action=post}")]
    public class ShortenApiController : ApiController
    {
        private readonly IUrlService _urlService;

        public ShortenApiController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        [ValidateModelState]
        [CheckNullModel]
        [HttpPost]
        public async Task<IHttpActionResult> Post(ShortenUrlViewModel model)
        {
            var shortenedUrlresponse = await _urlService.FindorCreateShortUrlAsync(model.Url);
            if (shortenedUrlresponse.Succeeded)
                return Ok(shortenedUrlresponse.Model);

            ControllerHelpers.AddErrors(shortenedUrlresponse, this);
            return BadRequest(ModelState);
        }
    }
}
