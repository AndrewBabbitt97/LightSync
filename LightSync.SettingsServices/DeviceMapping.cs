namespace LightSync.SettingsServices
{
    /// <summary>
    /// A device mapping
    /// </summary>
    public class DeviceMapping
    {
        /// <summary>
        /// The device to map lights from
        /// </summary>
        public string FromDevice { get; set; }

        /// <summary>
        /// The device to map lights to
        /// </summary>
        public string ToDevice { get; set; }

        /// <summary>
        /// The mapping mode
        /// </summary>
        public string MappingMode { get; set; }
    }
}
