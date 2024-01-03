namespace ProductRegistrationSystemAPI.data.cache
{
    public interface ICacheHelper
    {
        Dictionary<int,string>? GetDictionary(string key);
        void SetDictionary(string key, Dictionary<int,string> value);

    }
}
