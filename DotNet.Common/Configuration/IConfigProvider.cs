namespace DotNet.Common.Configuration
{
    public interface IConfigProvider
    {
        T LoadConfiguration<T>(string configStoreInfor) where T : class;

        string SaveConfiguration<T>(string configStoreInfor, T configInfor) where T : class;

        string GetCacheInfor(string configStoreInfor);
    }
}
