using LightSync.Providers.Asus;

namespace LightSync.Core
{
    /// <summary>
    /// The light sync extension methods
    /// </summary>
    public static class AsusDeviceProviderExtensions
    {
        /// <summary>
        /// Uses the ASUS device provider
        /// </summary>
        /// <param name="lightSync">The light sync instance to add the provider to</param>
        public static void UseAsus(this ILightSyncService lightSync)
        {
            lightSync.AddProvider(new AsusDeviceProvider());
        }
    }
}
