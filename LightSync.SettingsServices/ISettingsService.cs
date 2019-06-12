namespace LightSync.SettingsServices
{
    /// <summary>
    /// The settings service interface
    /// </summary>
    public interface ISettingsService
    {
        /// <summary>
        /// The settings
        /// </summary>
        Settings Settings { get; set; }

        /// <summary>
        /// Saves the settings
        /// </summary>
        void Save();

        /// <summary>
        /// Loads the settings
        /// </summary>
        void Load();
    }
}
