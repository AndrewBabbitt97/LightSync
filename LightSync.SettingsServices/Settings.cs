using System.Collections.Generic;

namespace LightSync.SettingsServices
{
    /// <summary>
    /// The light sync settings
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// The list of available devices
        /// </summary>
        public List<string> AvailableDevices { get; set; }

        /// <summary>
        /// The list of device mappings
        /// </summary>
        public List<DeviceMapping> DeviceMappings { get; set; }

        /// <summary>
        /// Creates a settings instance
        /// </summary>
        public Settings()
        {
            AvailableDevices = new List<string>();
            DeviceMappings = new List<DeviceMapping>();
        }
    }
}
