﻿using System;
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
    public partial class LeftForm : DockContent
    {
        public LeftForm()
        {
            InitializeComponent();
            Global.toc = this.axTOCControl1;
        }
    }
}
