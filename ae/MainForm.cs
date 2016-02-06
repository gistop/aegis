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
    }
}
