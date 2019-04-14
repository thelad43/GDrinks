namespace GDrinks.Tests
{
    using GDrinks.Common.Mapping;
    using GDrinks.Services;

    public class Tests
    {
        private static bool testsInitialized = false;

        public static void Initialize()
        {
            if (!testsInitialized)
            {
                AutoMapperConfig.RegisterMappings(typeof(IService).Assembly);
                testsInitialized = true;
            }
        }
    }
}