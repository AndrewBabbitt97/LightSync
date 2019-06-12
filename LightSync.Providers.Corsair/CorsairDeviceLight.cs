using System.Drawing;
using Corsair.CUE.SDK;
using LightSync.Core;

namespace LightSync.Providers.Corsair
{
    /// <summary>
    /// A Corsair device light
    /// </summary>
    class CorsairDeviceLight : IDeviceLight
    {
        /// <summary>
        /// The device lights color
        /// </summary>
        public Color Color { get => GetColor(); set => SetColor(value); }

        /// <summary>
        /// The device light
        /// </summary>
        internal CorsairLedColor _deviceLight;

        /// <summary>
        /// Creates a Corsair device light
        /// </summary>
        /// <param name="deviceLight">The device light</param>
        internal CorsairDeviceLight(CorsairLedColor deviceLight)
        {
            _deviceLight = deviceLight;
        }

        /// <summary>
        /// Gets the color
        /// </summary>
        /// <returns>The color</returns>
        private Color GetColor()
        {
            return Color.FromArgb(_deviceLight.r, _deviceLight.g, _deviceLight.b);
        }

        /// <summary>
        /// Sets the color
        /// </summary>
        /// <param name="value">The color</param>
        private void SetColor(Color value)
        {
            _deviceLight.r = value.R;
            _deviceLight.g = value.G;
            _deviceLight.b = value.B;
        }
    }
}
