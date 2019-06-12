using System;
using System.IO;
using System.Windows;
using LightSync.SettingsServices;

namespace LightSync.Config
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// The settings service
        /// </summary>
        public static ISettingsService SettingsService { get; set; }

        /// <summary>
        /// Creates the app
        /// </summary>
        public App()
        {
            var contentRoot = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                "LightSync");

            SettingsService = new SettingsService(contentRoot, false);
        }
    }
}
