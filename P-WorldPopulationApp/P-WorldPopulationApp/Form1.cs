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

namespace P_WorldPopulationApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Series s = new Series("test");
            s.ChartType = SeriesChartType.Line;
            this.worldpop.Series.Add(s);

            this.worldpop.Series[s.Name].Points.AddXY("Test1", 28);
            this.worldpop.Series[s.Name].Points.AddXY("Test2", 63);
            this.worldpop.Series[s.Name].Points.AddXY("Test3", 50);
            this.worldpop.Series[s.Name].Points.AddXY("Test4", 20);
            this.worldpop.Series[s.Name].Points.AddXY("Test5", 74);
            this.worldpop.Series[s.Name].Points.AddXY("Test6", 12);
            this.worldpop.Series[s.Name].Points.AddXY("Test7", 58);
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
