using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;
using Corsair.CUE.SDK;
using LightSync.Core;

namespace LightSync.Providers.Corsair
{
    /// <summary>
    /// The Corsair device provider
    /// </summary>
    class CorsairDeviceProvider : IDeviceProvider
    {
        /// <summary>
        /// The provider name
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The devices the provider has
        /// </summary>
        public IEnumerable<IDevice> Devices { get => _devices; }

        /// <summary>
        /// The devices the provider has
        /// </summary>
        private List<CorsairDevice> _devices;

        /// <summary>
        /// The CUE launcher process
        /// </summary>
        private Process _launcherProcess;

        /// <summary>
        /// Creates a Corsair device provider
        /// </summary>
        public CorsairDeviceProvider()
        {
            Name = "Corsair";
            _devices = new List<CorsairDevice>();

            _launcherProcess = new Process()
            {
                StartInfo = new ProcessStartInfo(
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Corsair\\CORSAIR iCUE Software\\iCUE Launcher.exe"),
                    "--autorun")
            };
        }

        /// <summary>
        /// Initializes the provider
        /// </summary>
        public void Initialize()
        {
            PerformHealthCheck();

            for (int i = 0; i < CUESDK.CorsairGetDeviceCount(); i++)
            {
                _devices.Add(new CorsairDevice(i));
            }
        }

        /// <summary>
        /// Performs a health check on the provider
        /// </summary>
        public void PerformHealthCheck()
        {
            var cueRunning = Process.GetProcessesByName("iCUE").Length != 0;

            while (!cueRunning)
            {
                _launcherProcess.Start();
                Thread.Sleep(10000);
                cueRunning = Process.GetProcessesByName("iCUE").Length != 0;
            }

            CUESDK.CorsairGetDeviceCount();
            var error = CUESDK.CorsairGetLastError();

            while (error == CorsairError.CE_ServerNotFound ||
                error == CorsairError.CE_ProtocolHandshakeMissing)
            {
                CUESDK.CorsairPerformProtocolHandshake();
                Thread.Sleep(100);
                error = CUESDK.CorsairGetLastError();
            }
        }

        /// <summary>
        /// Requests exclusive control over the provider
        /// </summary>
        public void RequestControl()
        {
            CUESDK.CorsairRequestControl(CorsairAccessMode.CAM_ExclusiveLightingControl);
        }

        /// <summary>
        /// Releases exclusive control over the provider
        /// </summary>
        public void ReleaseControl()
        {
            CUESDK.CorsairReleaseControl(CorsairAccessMode.CAM_ExclusiveLightingControl);
        }
    }
}
