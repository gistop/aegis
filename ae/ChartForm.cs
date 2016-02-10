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
using WeifenLuo.WinFormsUI.Docking;

namespace ae
{
    public partial class ChartForm : DockContent
    {
        public ChartForm()
        {
            InitializeComponent();
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            Series series = new Series("Spline");
            series.ChartType = SeriesChartType.Spline;
            series.BorderWidth = 3;
            series.ShadowOffset = 2;
            series.Points.AddY(67);
            series.Points.AddY(57);
            series.Points.AddY(83);
            series.Points.AddY(23);
            series.Points.AddY(70);
            series.Points.AddY(60);
            series.Points.AddY(90);
            series.Points.AddY(20);
            chart1.Series.Add(series);
        }
    }
}
