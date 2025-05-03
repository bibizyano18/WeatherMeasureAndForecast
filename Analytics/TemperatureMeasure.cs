using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherMeasureAndForecast.Analytics
{
    public class TemperatureMeasure
    {
        public List<decimal> temperature = new List<decimal>();

        public decimal tempMeasure(decimal max, decimal min)
        {
            decimal result = Math.Abs(max) - Math.Abs(min);
            return result;
        }

        // нужно добавить проход по массиву данных температур
    }
}
