using System.IO;
using System.Xml.Serialization;
using System.Threading;

namespace LightSync.SettingsServices
{
    /// <summary>
    /// The settings service
    /// </summary>
    public class SettingsService : ISettingsService
    {
        /// <summary>
        /// The settings
        /// </summary>
        public Settings Settings { get; set; }

        /// <summary>
        /// The location of the settings
        /// </summary>
        private string _settingsFileLocation;

        /// <summary>
        /// The settings file watcher
        /// </summary>
        private FileSystemWatcher _settingsFileWatcher;

        /// <summary>
        /// Creates a instance of the settings service
        /// </summary>
        /// <param name="settingsLocation">The location to store settings</param>
        /// <param name="enableHotReload">If hot reload should be enabled</param>
        public SettingsService(string settingsLocation, bool enableHotReload = true)
        {
            Settings = new Settings();
            _settingsFileLocation = Path.Combine(settingsLocation, "settings.xml");
            _settingsFileWatcher = new FileSystemWatcher(settingsLocation, "settings.xml");
            _settingsFileWatcher.NotifyFilter = NotifyFilters.LastWrite;
            _settingsFileWatcher.Changed += SettingsFileWatcher_Changed;
            _settingsFileWatcher.Created += SettingsFileWatcher_Changed;
            _settingsFileWatcher.EnableRaisingEvents = enableHotReload;
            Load();
        }

        /// <summary>
        /// Saves the settings
        /// </summary>
        public void Save()
        {
            try
            {
                using (TextWriter writer = new StreamWriter(_settingsFileLocation, false))
                {
                    var serializer = new XmlSerializer(typeof(Settings));
                    serializer.Serialize(writer, Settings);
                }
            }
            catch (System.Exception) { }
        }

        /// <summary>
        /// Loads the settings
        /// </summary>
        public void Load()
        {
            try
            {
                if (File.Exists(_settingsFileLocation))
                {
                    using (TextReader reader = new StreamReader(_settingsFileLocation))
                    {
                        var serializer = new XmlSerializer(typeof(Settings));
                        Settings = (Settings)serializer.Deserialize(reader);
                    }
                }
            } catch (System.Exception) { }
        }

        /// <summary>
        /// Occurs when the settings file is updated
        /// </summary>
        /// <param name="sender">The sending object</param>
        /// <param name="e">The arguments</param>
        private void SettingsFileWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            Thread.Sleep(100);
            Load();
        }
    }
}
