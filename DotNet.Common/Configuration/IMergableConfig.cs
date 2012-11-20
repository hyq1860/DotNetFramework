namespace DotNet.Common.Configuration
{
    public interface IMergableConfig
    {
        void Merge(IMergableConfig config);
    }
}
