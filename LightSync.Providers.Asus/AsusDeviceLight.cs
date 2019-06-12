using System.Drawing;
using AuraServiceLib;
using LightSync.Core;

namespace LightSync.Providers.Asus
{
    /// <summary>
    /// A ASUS device light
    /// </summary>
    class AsusDeviceLight : IDeviceLight
    {
        /// <summary>
        /// The device lights color
        /// </summary>
        public Color Color { get => GetColor(); set => SetColor(value); }

        /// <summary>
        /// The device light
        /// </summary>
        internal IAuraRgbLight _deviceLight;

        /// <summary>
        /// Creates a ASUS device light
        /// </summary>
        /// <param name="deviceLight">The device light</param>
        internal AsusDeviceLight(IAuraRgbLight deviceLight)
        {
            _deviceLight = deviceLight;
        }

        /// <summary>
        /// Gets the color
        /// </summary>
        /// <returns>The color</returns>
        private Color GetColor()
        {
            return Color.FromArgb(_deviceLight.Red, _deviceLight.Green, _deviceLight.Blue);
        }

        /// <summary>
        /// Sets the color
        /// </summary>
        /// <param name="value">The color</param>
        private void SetColor(Color value)
        {
            _deviceLight.Red = value.R;
            _deviceLight.Green = value.G;
            _deviceLight.Blue = value.B;
        }
    }
}
