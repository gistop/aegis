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
    public partial class AttributionForm : DockContent
    {
        public AttributionForm()
        {
            InitializeComponent();
            Global.dgvattribution = this.dataGridView1;
        }

        private void 添加字段ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Datamanagement dt = new Datamanagement();
            dt.addField("test","string");
        }
    }
}
