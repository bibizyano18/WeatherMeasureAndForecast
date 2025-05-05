using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



/* Пример файла (Дата, минимальная, максимальная, Прогноз)
2025-04-01,2,10,Облачно
2025 - 04 - 02,1,9,Ясно
2025-04-03,3,12, Дождь
2025-04-04,4,13, Облачно
2025-04-05,5,14, Ясно */

namespace WeatherMeasureAndForecast
{
    public class WeatherDataReader
    {
        public List<DataTemperature> dataTemperatures = new List<DataTemperature>();
        public List<DataTemperature> LoadFromFile(string filePath) 
        {
            var lines = File.ReadAllLines(filePath);  
            
            for (int i = 0; i<lines.Length; i++)
            {
                var line = lines[i].Split(',');

                if (line.Length < 4)
                    continue;

                try
                {
                    var day = new DataTemperature
                    {
                        Date = DateTime.Parse(line[0]),
                        minTemp = double.Parse(line[1]),
                        maxTemp = double.Parse(line[2]),
                        Description = line[3]
                    };
                    dataTemperatures.Add(day);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка на строке {i+1}: {ex.Message}");
                }
            }
            
            return dataTemperatures;
        }

        public List<double> makeTemperatureList()
        {
            var res = new List<double>();
            for (int i =0; i< dataTemperatures.Count; i++)
            {
                res.Add(dataTemperatures[i].avgTemp);
            }

            return res;
        }
    }
}
