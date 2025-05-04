using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherMeasureAndForecast
{
    public class DataTemperature
    {
        public DateTime Date { get; set; }
        public double maxTemp { get; set; }
        public double minTemp { get; set; }
        public double avgTemp => (minTemp + maxTemp) / 2.0; 
        public string Description { get; set; }
    }
}
