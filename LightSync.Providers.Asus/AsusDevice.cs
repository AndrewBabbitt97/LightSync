using System.Collections.Generic;
using AuraServiceLib;
using LightSync.Core;

namespace LightSync.Providers.Asus
{
    /// <summary>
    /// A ASUS device
    /// </summary>
    class AsusDevice : IDevice
    {
        /// <summary>
        /// The device name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The lights the device has
        /// </summary>
        public IEnumerable<IDeviceLight> Lights { get => _lights; }

        /// <summary>
        /// The device
        /// </summary>
        private IAuraSyncDevice _device;

        /// <summary>
        /// The lights the device has
        /// </summary>
        private List<AsusDeviceLight> _lights;

        /// <summary>
        /// Creates a ASUS device
        /// </summary>
        /// <param name="device">The device</param>
        internal AsusDevice(IAuraSyncDevice device)
        {
            _device = device;
            Name = _device.Name;
            _lights = new List<AsusDeviceLight>();

            foreach (IAuraRgbLight light in _device.Lights)
            {
                _lights.Add(new AsusDeviceLight(light));
            }
        }

        /// <summary>
        /// Applies light changes to the device
        /// </summary>
        public void ApplyLights()
        {
            _device.Apply();
        }
    }
}
