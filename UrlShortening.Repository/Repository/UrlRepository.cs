using System;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
using UrlShortening.Domain.Model;
using UrlShortening.Infrastructure.IInfrastructure;
using UrlShortening.Repository.IRepository;

namespace UrlShortening.Repository.Repository
{
    public class UrlRepository : IUrlRepository
    {
        private readonly IConnection _conn;
        private readonly ILogger _logger;

        public UrlRepository(IConnection conn,
            ILogger logger)
        {
            _conn = conn;
            _logger = logger;
        }

        public async Task<Response> CreateAsync(UrlModel item)
        {
            var returnValue = new Response();
            try
            {
                using (var dc = _conn.GetContext())
                {
                    var localobject = Mapper.Map<urls>(item);
                    dc.urls.Add(localobject);
                    await dc.SaveChangesAsync();

                    var resultSet = Mapper.Map<UrlModel>(localobject);

                    returnValue.Model = resultSet;
                }
            }
            catch (Exception e)
            {
                // Log exception and compose Error message
                _logger.LogError("Create URL Record Error : ", e);
                returnValue.Succeeded = false;
                returnValue.Errors.Add("Unexpected error occurred saving the url.");
            }
            return returnValue;
        }

        public async Task<Response> RetrieveAsync(int id)
        {
            var returnValue = new Response();
            try
            {
                using (var dc = _conn.GetContext())
                {
                    var retrivedObject = await 
                        dc.urls.FindAsync(id);
                    var resultSet = Mapper.Map<UrlModel>(retrivedObject);

                    returnValue.Model = resultSet;
                }
            }
            catch (Exception e)
            {
                // Log exception and compose Error message
                _logger.LogError("Retrieve URL Error : ", e);
                returnValue.Succeeded = false;
                returnValue.Errors.Add("Unexpected error occurred retrieving the url.");
            }
            return returnValue;
        }


        public async Task<Response> RetrieveByUrlAsync(string url)
        {
            var returnValue = new Response();
            try
            {
                using (var dc = _conn.GetContext())
                {
                    var retrivedObject = await dc.urls.FirstOrDefaultAsync(a => a.actualUrl.Equals(url));
                    var resultSet = Mapper.Map<UrlModel>(retrivedObject);

                    returnValue.Model = resultSet;
                }
            }
            catch (Exception e)
            {
                // Log exception and compose Error message
                _logger.LogError("Retrieve URL by Actual Url Error : ", e);
                returnValue.Succeeded = false;
                returnValue.Errors.Add("Unexpected error occurred finding the url.");
            }
            return returnValue;
        }
    }
}