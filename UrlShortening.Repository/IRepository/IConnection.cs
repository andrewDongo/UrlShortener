namespace UrlShortening.Repository.IRepository
{
    public interface IConnection
    {
        url_shorteningEntities GetContext();
    }
}