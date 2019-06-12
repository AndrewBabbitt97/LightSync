using System;
using System.Windows;
using LightSync.SettingsServices;

namespace LightSync.Config
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Creates the main window
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            DeviceMappings.ItemsSource = App.SettingsService.Settings.DeviceMappings;
            FromDeviceColumn.ItemsSource = App.SettingsService.Settings.AvailableDevices;
            ToDeviceColumn.ItemsSource = App.SettingsService.Settings.AvailableDevices;
            MappingModeColumn.ItemsSource = Enum.GetNames(typeof(MappingMode));
        }

        /// <summary>
        /// Saves the settings
        /// </summary>
        /// <param name="sender">The sending object</param>
        /// <param name="e">The arguments</param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            App.SettingsService.Save();
            MessageBox.Show("Settings saved succesfully!");
        }
    }
}
