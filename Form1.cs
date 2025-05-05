using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
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


                if (chart2.Series.IndexOf("Прогноз") != -1)
                    chart2.Series.Remove(chart2.Series["Прогноз"]);

                if (chart2.Series.IndexOf("Series1") != -1)
                    chart2.Series.Remove(chart2.Series["Series1"]);

                // Новая серия для chart2
                var forecastSeries = new Series("Прогноз")
                {
                    ChartType = SeriesChartType.Line,
                    Color = Color.Green,
                    BorderDashStyle = ChartDashStyle.Dash,
                    BorderWidth = 2
                };

                // Парсим строки richTextBox2
                var lines = richTextBox2.Lines;
                foreach (var line in lines)
                {
                    // Ожидаемый формат: 01.05.2025 температура - 14.2 °C
                    var parts = line.Split(' ');
                    if (parts.Length >= 4 &&
                        DateTime.TryParseExact(parts[0], "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date) &&
                        double.TryParse(parts[3], out double temp))
                    {
                        forecastSeries.Points.AddXY(date.ToShortDateString(), temp);
                    }
                }

                // Добавляем серию на chart2
                chart2.Series.Add(forecastSeries);

            }
            else { MessageBox.Show("Введите корректное число"); }

        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "TXT файлы (*txt)|*.txt|Все файлы (*.*)|*.*";
            ofd.Title = "Выберите файл с погодой";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                
                var data = reader.LoadFromFile(ofd.FileName);
                // Петя вот тут добавляешь в табличку или ListView
               
                MessageBox.Show($"Загружено записей: {data.Count}");
            }

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = reader.dataTemperatures;

            chart1.Series.Clear();
            var seriesMin = new Series("Мин. температура") { ChartType = SeriesChartType.Line, Color = Color.Blue };
            var seriesMax = new Series("Макс. температура") { ChartType = SeriesChartType.Line, Color = Color.Red };
            foreach (var d in reader.dataTemperatures)
            {
                seriesMin.Points.AddXY(d.Date.ToShortDateString(), d.minTemp);
                seriesMax.Points.AddXY(d.Date.ToShortDateString(), d.maxTemp);
            }
            chart1.Series.Add(seriesMin);
            chart1.Series.Add(seriesMax);

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["minTemp"].Value != null && row.Cells["maxTemp"].Value != null)
                {
                    row.Cells["minTemp"].Style.BackColor = Color.LightBlue;
                    row.Cells["maxTemp"].Style.BackColor = Color.LightCoral;
                }
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
