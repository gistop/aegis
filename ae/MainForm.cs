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
                case "2":
                    //dayName = "Tuesday";
                    break;
                default:
                    //dayName = "Unknown";
                    break;
            }
        }
        //buttonItem_Clickend
    }
}
