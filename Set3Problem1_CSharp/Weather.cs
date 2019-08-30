using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Set3Problem1_CSharp
{
    public sealed class Weather
    {
        private readonly WeatherType _weatherType;
        private readonly double _potholeFactor;

        public Weather(WeatherType weatherType, double potholeFactor)
        {
            _weatherType = weatherType;
            _potholeFactor = potholeFactor;
        }

        #region Data Readers

        public string GetWeatherType() => _weatherType.ToString();
        public double GetPotholeFactor() => _potholeFactor;

        #endregion
    }
}
