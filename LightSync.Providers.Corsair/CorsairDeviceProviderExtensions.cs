using LightSync.Providers.Corsair;

namespace LightSync.Core
{
    /// <summary>
    /// The light sync extension methods
    /// </summary>
    public static class CorsairDeviceProviderExtensions
    {
        /// <summary>
        /// Uses the Corsair device provider
        /// </summary>
        /// <param name="lightSync">The light sync instance to add the provider to</param>
        public static void UseCorsair(this ILightSyncService lightSync)
        {
            lightSync.AddProvider(new CorsairDeviceProvider());
        }
    }
}
