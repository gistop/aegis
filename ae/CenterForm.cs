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
    public partial class CenterForm : DockContent
    {

        private AttributionForm attributionform = new AttributionForm();
        private LayoutForm layoutForm = new LayoutForm();
        private MapForm mapForm = new MapForm();
        private ChartForm chartForm = new ChartForm();
        private RightForm rightForm = new RightForm();

        
        public CenterForm()
        {
            InitializeComponent();

            attributionform.Show(dockPanel2, DockState.DockBottom);
            mapForm.Show(dockPanel2);
            layoutForm.Show(dockPanel2);
            chartForm.Show(dockPanel2);
            rightForm.Show(dockPanel2, DockState.DockRight);
            mapForm.Activate(); //激活项
            
        }
    }
}
