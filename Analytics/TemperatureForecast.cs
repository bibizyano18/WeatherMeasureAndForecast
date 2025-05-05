using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherMeasureAndForecast.Analytics
{
    public class TemperatureForecast
    {
        public List<double> temperature = new List<double>
        {
            15.5, 16.2, 13, 18.4, 19.2, 14.7
        };

        public void TempForecast(List<double> temps, List<double> res, int n)
        {
            if (n > temps.Count)
            {
                throw new ArgumentException("n превышает кол-во дней");
            }
            double sum = 0;
            for (int i = temps.Count - 1; i >= temps.Count - n; i--)
            {
                sum += temps[i];
            }
            temps.Add(sum / n);
            res.Add(sum / n);
            if (res.Count == n)
            {
                return;
            }
            else { TempForecast(temps, res, n); }
        }

        // нужно добавить проход по массиву данных температур
    }
}
