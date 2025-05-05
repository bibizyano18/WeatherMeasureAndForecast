using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherMeasureAndForecast.Analytics
{
    public class TemperatureForecast
    {
        public List<double> temperature = new List<double>
        {
            15.5, 16.2, 13, 18.4, 19.2, 14.7
        };

        public decimal TempForecast(decimal max, decimal min)
        {
            decimal result = Math.Abs(max) - Math.Abs(min);
            return result;
        }

        // нужно добавить проход по массиву данных температур
    }
}
