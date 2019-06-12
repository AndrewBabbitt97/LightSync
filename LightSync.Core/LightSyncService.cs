using System.Linq;
using System.Collections.Generic;

namespace LightSync.Core
{
    /// <summary>
    /// The light sync service
    /// </summary>
    public class LightSyncService : ILightSyncService
    {
        /// <summary>
        /// The list of device providers
        /// </summary>
        public IEnumerable<IDeviceProvider> DeviceProviders { get => _deviceProviders; }

        /// <summary>
        /// The device providers
        /// </summary>
        private List<IDeviceProvider> _deviceProviders;

        /// <summary>
        /// Creates a instance of the light sync service
        /// </summary>
        public LightSyncService()
        {
            _deviceProviders = new List<IDeviceProvider>();
        }

        /// <summary>
        /// Adds a device provider to the service
        /// </summary>
        /// <param name="provider">The provider to add</param>
        public void AddProvider(IDeviceProvider provider)
        {
            _deviceProviders.Add(provider);
        }

        /// <summary>
        /// Initializes the light syncvice
        /// </summary>
        public void Initialize()
        {
            foreach (var provider in DeviceProviders)
            {
                provider.Initialize();

                foreach (var device in provider.Devices)
                {
                    var duplicateDevices = provider.Devices.Where(d => d.Name == device.Name).ToArray();

                    if (duplicateDevices.Length > 1)
                    {
                        for (int i = 0; i < duplicateDevices.Length; i++)
                        {
                            duplicateDevices[i].Name = duplicateDevices[i].Name + " " + (i + 1);
                        }
                    }
                }
            }
        }
    }
}
