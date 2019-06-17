using System;
using System.Timers;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using LightSync.Core;
using LightSync.SettingsServices;

namespace LightSync
{
    /// <summary>
    /// The light sync worker
    /// </summary>
    public class Worker : BackgroundService
    {
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<Worker> _logger;

        /// <summary>
        /// The light sync service
        /// </summary>
        private readonly ILightSyncService _lightSync;

        /// <summary>
        /// The settings service
        /// </summary>
        private readonly ISettingsService _settings;

        /// <summary>
        /// The health check timer
        /// </summary>
        private System.Timers.Timer _healthCheckTimer;

        /// <summary>
        /// Creates the worker
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <param name="lightSync">The light sync service</param>
        /// <param name="settings">The light sync service</param>
        public Worker(ILogger<Worker> logger, ILightSyncService lightSync, ISettingsService settings)
        {
            _logger = logger;
            _lightSync = lightSync;
            _settings = settings;
            _healthCheckTimer = new System.Timers.Timer(15000);
            _healthCheckTimer.Elapsed += HealthCheckTimer_Elapsed;
        }

        /// <summary>
        /// Executes the worker
        /// </summary>
        /// <param name="stoppingToken">The stopping token</param>
        /// <returns>A task</returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(1, stoppingToken);

            _lightSync.Initialize();

            _settings.Settings.AvailableDevices.Clear();

            foreach (var provider in _lightSync.DeviceProviders)
            {
                foreach (var device in provider.Devices)
                {
                    _settings.Settings.AvailableDevices.Add(provider.Name + "/" + device.Name);
                }
            }

            _settings.Save();

            _healthCheckTimer.Start();

            _logger.LogInformation("LightSync started successfully...");

            while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var deviceMapping in _settings.Settings.DeviceMappings)
                {
                    var fromStrings = deviceMapping.FromDevice.Split('/');
                    var toStrings = deviceMapping.ToDevice.Split('/');

                    var fromProvider = _lightSync.DeviceProviders.Where(p => p.Name == fromStrings[0]).FirstOrDefault();

                    if (fromProvider == null)
                        continue;

                    var fromDevice = fromProvider.Devices.Where(d => d.Name == fromStrings[1]).FirstOrDefault();

                    if (fromDevice == null)
                        continue;

                    var toProvider = _lightSync.DeviceProviders.Where(p => p.Name == toStrings[0]).FirstOrDefault();

                    if (toProvider == null)
                        continue;

                    var toDevice = toProvider.Devices.Where(d => d.Name == toStrings[1]).FirstOrDefault();

                    if (toDevice == null)
                        continue;

                    toProvider.RequestControl();

                    switch (Enum.Parse<MappingMode>(deviceMapping.MappingMode))
                    {
                        case MappingMode.SingleColor:
                            ApplySingleColorEffect(fromDevice, toDevice);
                            break;
                        case MappingMode.Wrap:
                            ApplyWrapEffect(fromDevice, toDevice);
                            break;
                        default:
                            break;
                    }
                }

                await Task.Delay(1, stoppingToken);
            }

            _healthCheckTimer.Stop();
        }

        /// <summary>
        /// Occurs during a health check cycle
        /// </summary>
        /// <param name="sender">The sending object</param>
        /// <param name="e">The arguments</param>
        private void HealthCheckTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            foreach (var provider in _lightSync.DeviceProviders)
            {
                provider.PerformHealthCheck();
            }
        }

        /// <summary>
        /// Applies a single color effect to a device
        /// </summary>
        /// <param name="fromDevice">The device to map lights from</param>
        /// <param name="toDevice">The device to map lights to</param>
        private void ApplySingleColorEffect(IDevice fromDevice, IDevice toDevice)
        {
            var fromLight = fromDevice.Lights.FirstOrDefault();

            foreach (var light in toDevice.Lights)
            {
                light.Color = fromLight.Color;
            }

            toDevice.ApplyLights();
        }

        /// <summary>
        /// Applies a wrap effect to a device
        /// </summary>
        /// <param name="fromDevice">The device to map lights from</param>
        /// <param name="toDevice">The device to map lights to</param>
        private void ApplyWrapEffect(IDevice fromDevice, IDevice toDevice)
        {
            var fromLights = fromDevice.Lights.ToArray();
            var currentLight = 0;

            foreach (var light in toDevice.Lights)
            {
                light.Color = fromLights[currentLight].Color;
                currentLight++;

                if (currentLight == fromLights.Length)
                    currentLight = 0;
            }

            toDevice.ApplyLights();
        }
    }
}
