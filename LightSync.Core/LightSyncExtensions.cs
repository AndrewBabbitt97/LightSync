using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace LightSync.Core
{
    /// <summary>
    /// The light sync extension methods
    /// </summary>
    public static class LightSyncExtensions
    {
        /// <summary>
        /// Configures light sync
        /// </summary>
        /// <param name="builder">The host builder</param>
        /// <param name="lightSyncDelegate">The action to execute upon configuring</param>
        /// <returns>The original host builder for chaining</returns>
        public static IHostBuilder ConfigureLightSync(this IHostBuilder builder, Action<ILightSyncService> lightSyncDelegate)
        {
            var lightSync = new LightSyncService();

            lightSyncDelegate(lightSync);

            return builder.ConfigureServices(services =>
            {
                services.AddSingleton<ILightSyncService>(lightSync);
            });
        }
    }
}
