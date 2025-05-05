using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeatherMeasureAndForecast.Analytics;

namespace WeatherMeasureAndForecast
{

    public partial class Form1 : Form
    {
        WeatherDataReader reader = new WeatherDataReader();
        TemperatureForecast TemperatureForecast = new TemperatureForecast();
        public Form1()
        {
            InitializeComponent();
            
        }

        private void btnForecast_Click(object sender, EventArgs e)
        {
            if (reader.dataTemperatures.Count == 0)
            {
                MessageBox.Show("Прочитайте файл");
                return;
            }
            if (numericUpDown1.Value > 0 && numericUpDown1.Value <= reader.dataTemperatures.Count)
            {
                var temps = reader.makeTemperatureList();
                var res = new List<double>();
                TemperatureForecast.TempForecast(temps, res, (int)numericUpDown1.Value);
                richTextBox2.Clear();
                for (int i = 0; i < res.Count; i++)
                {
                    richTextBox2.Text += $"{reader.dataTemperatures[reader.dataTemperatures.Count-1].Date.AddDays(i+1):dd.MM.yyyy} температура - {Math.Round(res[i], 1)} °C \n";
                }
            }
            else { MessageBox.Show("Введите корректное число"); }

        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "TXT файлы (*txt)|*.txt|Все файлы (*.*)|*.*";
            ofd.Title = "Выберите файл с погдой";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                
                var data = reader.LoadFromFile(ofd.FileName);
                // Петя вот тут добавляешь в табличку или ListView
               
                MessageBox.Show($"Загружено записей: {data.Count}");
            }
        }

        private void btnAvg_Click(object sender, EventArgs e)
        {
            if (reader.dataTemperatures.Count == 0)
            {
                MessageBox.Show("Прочитайте файл");
                return;
            }
            richTextBox1.Clear();
            double minP=1000000, maxP=0;
            DateTime maxD = DateTime.Now, minD = DateTime.Now;
            for (int i = 0; i< reader.dataTemperatures.Count; i++)
            {
                var avg = reader.dataTemperatures[i].maxTemp - reader.dataTemperatures[i].minTemp;
                if ( avg < minP)
                {
                    minP = avg;
                    minD = reader.dataTemperatures[i].Date;
                }
                else if (avg > maxP)
                {
                    maxP = avg;
                    maxD = reader.dataTemperatures[i].Date;
                }
            }
            richTextBox1.Text += $"Самая сильный перепад температуры {maxD:dd.MM.yyyy} - {maxP} °C \n Самый слабый перепад температуры {minD:dd.MM.yyyy} - {minP} °C \n";


        }
    }
}
