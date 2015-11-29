using UrlShortening.Repository.IRepository;

namespace UrlShortening.Repository.Repository
{

    public class Connection : IConnection
    {
        /// <summary>
        /// Returns instance of databse context
        /// </summary>
        /// <returns>url_shorteningEntities DataContext</returns>
        public url_shorteningEntities GetContext()
        {
            var contextEntities = new url_shorteningEntities();
            return contextEntities;
        } 
    }
}