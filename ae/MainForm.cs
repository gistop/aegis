using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ae
{
    public partial class MainForm : Form
    {

        #region 属性字段
        private LeftForm leftWin = new LeftForm();
        private CenterForm centerWin = new CenterForm();
        #endregion
        
        
        public MainForm()
        {
            InitializeComponent();


            leftWin.Show(dockPanel1, DockState.DockLeft);
            centerWin.Show(dockPanel1);

            axToolbarControl1.SetBuddyControl(Global.mainmap);
            Global.maptoolbar = axToolbarControl1;


        }

        private void buttonItem_Click(object sender, EventArgs e)
        {
            string tag = ((ButtonItem)sender).Tag.ToString();
            switch (tag)
            {
                case "home":
                    Home h = new Home();
                    h.HomeOperation((ButtonItem)sender);
                    break;
                case "1":
                    //dayName = "Monday";
                    break;
                case "query":
                    Home h1 = new Home();
                    h1.HomeOperation((ButtonItem)sender);
                    break;
                default:
                    //dayName = "Unknown";
                    break;
            }
        }

        private void buttonItem16_Click(object sender, EventArgs e)
        {
            Thematicmap tm = new Thematicmap();
            ESRI.ArcGIS.Display.IColor rgbColor = new ESRI.ArcGIS.Display.RgbColorClass();
            rgbColor.RGB = 255;
             ESRI.ArcGIS.Display.IColor rgbColor1 = new ESRI.ArcGIS.Display.RgbColorClass();
            rgbColor1.RGB = 100;
            tm.createSimpleFillSymbol("boundary", ESRI.ArcGIS.Display.esriSimpleFillStyle.esriSFSDiagonalCross, rgbColor, rgbColor1,"AREA","test");
            ESRI.ArcGIS.Carto.IActiveView pActiveView = Global.mainmap.Map as ESRI.ArcGIS.Carto.IActiveView;
            pActiveView.Refresh();
            Global.toc.Update();
        }

        private void buttonItem18_MouseEnter(object sender, EventArgs e)
        {
            Global.keyword = tbikeyword.TextBox.Text;
        }
        //buttonItem_Clickend
    }
}
