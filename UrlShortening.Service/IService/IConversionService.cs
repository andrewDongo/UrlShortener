namespace UrlShortening.Service.IService
{
    public interface IConversionService
    {
        string LongToBase62String(long value);
        long Base62StringToLong(string input);
    }

}