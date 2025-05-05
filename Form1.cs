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
            if (numericUpDown1.Value > 0 && numericUpDown1.Value <= 10)
            {
                var temps = TemperatureForecast.temperature;
                var res = new List<double>();
                TemperatureForecast.TempForecast(temps, res, (int)numericUpDown1.Value);
                richTextBox2.Clear();
                for (int i = 0; i < res.Count; i++)
                {
                    richTextBox2.Text += i+1 + " april " + Math.Round(res[i], 1) + "\n";
                }
            }
            else { MessageBox.Show("Введите корректное число"); }

        }
    }
}
