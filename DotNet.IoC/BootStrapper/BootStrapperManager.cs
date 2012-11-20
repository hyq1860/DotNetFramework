namespace DotNet.IoC
{
    public class BootStrapperManager
    {
        private static CommonBootStrapper _bootStrapper;

        public static void Initialize(CommonBootStrapper bootStrapper)
        {
            _bootStrapper = bootStrapper;
        }

        public static CommonBootStrapper BootStrapper
        {
            get { return _bootStrapper; }
        }
    }
}
