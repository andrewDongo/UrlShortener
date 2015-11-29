namespace UrlShortening.Domain.Model
{
    public class UrlModel
    {
        public long Id { get; set; }
        public string ActualUrl { get; set; }
        public int Visits { get; set; }
        public string ShortUrl { get; set; }
    }
}