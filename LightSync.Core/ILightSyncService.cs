using System.Collections.Generic;

namespace LightSync.Core
{
    /// <summary>
    /// The light sync service definition
    /// </summary>
    public interface ILightSyncService
    {
        /// <summary>
        /// The list of device providers
        /// </summary>
        IEnumerable<IDeviceProvider> DeviceProviders { get; }

        /// <summary>
        /// Adds a device provider to the service
        /// </summary>
        /// <param name="provider">The provider to add</param>
        void AddProvider(IDeviceProvider provider);

        /// <summary>
        /// Initializes the light syncvice
        /// </summary>
        void Initialize();
    }
}
