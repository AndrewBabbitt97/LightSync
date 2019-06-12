using System.Collections.Generic;

namespace LightSync.Core
{
    /// <summary>
    /// The device interface
    /// </summary>
    public interface IDevice
    {
        /// <summary>
        /// The device name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// The lights the device has
        /// </summary>
        IEnumerable<IDeviceLight> Lights { get; }

        /// <summary>
        /// Applies light changes to the device
        /// </summary>
        void ApplyLights();
    }
}
