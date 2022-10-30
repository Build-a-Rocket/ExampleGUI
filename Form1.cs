using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace ExampleGUI
{
    public partial class Form1 : Form
    {
        Random r;
        public Form1()
        {
            InitializeComponent();

            realTimeChart1.setTitle("Random Data 1");

            realTimeChart1.AddChart("Points", "Number", "Random", SeriesChartType.FastLine);

            realTimeChart2.setTitle("Random Data 2");

            realTimeChart2.AddChart("Points", "Number", "Random", SeriesChartType.FastLine);
            r = new Random();

            try
            {
                serialPort.Open();
            }
            catch { }
        }

        private void loadFile_Clicked(object sender, EventArgs e)
        {
            string[] lines = File.ReadAllLines(fileNameBox.Text);

            foreach (string line in lines)
            {
                outputBox.AppendText(line + "\r\n");
            }
        }

        private void saveFile_Clicked(object sender, EventArgs e)
        {
            File.WriteAllText(fileNameBox.Text, inputBox.Text);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (!serialPort.IsOpen)
            {
                realTimeChart1.AddPoint(0, r.NextDouble(), "Random");
                realTimeChart2.AddPoint(0, r.NextDouble(), "Random");
            }
        }

        private void dataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string line = serialPort.ReadLine();
            outputBox.AppendText(line);

            string[] data = line.Split(',');

            realTimeChart1.AddPoint(0, Convert.ToDouble(data[0]), "Random");
            realTimeChart2.AddPoint(0, Convert.ToDouble(data[0]), "Random");
        }
    }
}
