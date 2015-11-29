using AutoMapper;
using UrlShortening.Domain.Model;

namespace UrlShortening.Web.Models.Mapping
{
    public static class MapperWebConfiguration
    {

        public static void Configure()
        {
            ConfigureUrlMapping();
        }

        private static void ConfigureUrlMapping()
        {
            Mapper.CreateMap<string, UrlModel>()
                .ForMember(dest => dest.ActualUrl, opt => opt.MapFrom(src => src));
        }
    }
}