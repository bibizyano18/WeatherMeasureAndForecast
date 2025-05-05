using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherMeasureAndForecast
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "TXT файлы (*txt)|*.txt|Все файлы (*.*)|*.*";
            ofd.Title = "Выберите файл с погдой";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                var reader = new WeatherDataReader();
                var data = reader.LoadFromFile(ofd.FileName);
                // Петя вот тут добавляешь в табличку или ListView

                MessageBox.Show($"Загружено записей: {data.Count}");
            }
        }

        private void btnAvg_Click(object sender, EventArgs e)
        {

        }
    }
}
