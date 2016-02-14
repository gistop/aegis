using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PropertiesForm
{
    public partial class PropertiesUC: UserControl
    {
        public PropertiesUC()
        {
            InitializeComponent();
        }

        //调用主程序函数
        public delegate void PropertiesEvents();
        public event PropertiesEvents ApplyEvents;
        private void button3_Click(object sender, EventArgs e)
        {
            if (ApplyEvents != null)
            {
                ApplyEvents();
            }
        }
    }
}
