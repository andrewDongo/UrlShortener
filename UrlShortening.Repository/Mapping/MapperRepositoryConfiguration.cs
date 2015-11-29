using AutoMapper;
using UrlShortening.Domain.Model;

namespace UrlShortening.Repository.Mapping
{
    public static class MapperRepositoryConfiguration
    {
        public static void Configure()
        {
            ConfigureUrlMapping();
        }

        private static void ConfigureUrlMapping()
        {
            Mapper.CreateMap<UrlModel, urls>();
            Mapper.CreateMap<urls, UrlModel>();
        }
    }
}