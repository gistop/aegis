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
    public partial class MapForm : DockContent
    {
        public MapForm()
        {
            InitializeComponent();
            Global.toc.SetBuddyControl(this.axMapControl1);
            Global.mainmap = this.axMapControl1;
        }

        private void axMapControl1_OnAfterScreenDraw(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnAfterScreenDrawEvent e)
        {
            Datamanagement dt = new Datamanagement();
            dt.copyToPageLayout();
        }
    }
}
