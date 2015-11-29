using System.ComponentModel.DataAnnotations;

namespace UrlShortening.Web.Models
{
    public class ShortenUrlViewModel : HoneypotViewModel
    {
        [Required(ErrorMessage = "Please provide a {0}")]
        [DataType(DataType.Url)]
        public string Url { get; set; }
    }
}