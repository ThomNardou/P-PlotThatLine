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
        public Series s;
        public Form1()
        {
            InitializeComponent();
            this.s = new Series("test");
            s.ChartType = SeriesChartType.Line;
            this.worldpop.Series.Add(s);


            for (int i = 0; i < 10; i++)
            {
                DataPoint dp = new DataPoint();
                dp.SetValueXY($"Test{i}", 12 + 1);
                dp.ToolTip = "Hello from #VALX";
                this.worldpop.Series[this.s.Name].Points.Add(dp);
            }

    
            //this.worldpop.Series[s.Name].Points.AddXY("Test1", 28);
            //this.worldpop.Series[s.Name].Points.AddXY("Test2", 63);
            //this.worldpop.Series[s.Name].Points.AddXY("Test3", 50);
            //this.worldpop.Series[s.Name].Points.AddXY("Test4", 20);
            //this.worldpop.Series[s.Name].Points.AddXY("Test5", 74);
            //this.worldpop.Series[s.Name].Points.AddXY("Test6", 12);
            //this.worldpop.Series[s.Name].Points.AddXY("Test7", 58);
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //this.worldpop.Series[this.s.Name].Points.AddXY("Test8", 12);
            //this.worldpop.Series[this.s.Name].Points.AddXY("Test9", 1);
            //this.worldpop.Series[this.s.Name].Points.AddXY("Test10", 78);
            //this.worldpop.Series[this.s.Name].Points.AddXY("Test11", 51);
            //this.worldpop.Series[this.s.Name].Points.AddXY("Test12", 2);
            //this.worldpop.Series[this.s.Name].Points.AddXY("Test13", 12);
            //this.worldpop.Series[this.s.Name].Points.AddXY("Test14", 12);
            //this.worldpop.Series.
        }
    }
}
