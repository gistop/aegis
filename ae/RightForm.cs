using ESRI.ArcGIS.Geometry;
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
    public partial class RightForm : DockContent
    {
        public RightForm()
        {
            InitializeComponent();

            treeView1.LabelEdit = false;//不可编辑
            //添加结点
            TreeNode root = new TreeNode();
            root.Text = "工具箱";
            //一级
            TreeNode node1 = new TreeNode();
            node1.Text = "分析工具";
            //node1.ImageIndex = node1.SelectedImageIndex = 
            TreeNode node2 = new TreeNode();
            node2.Text = "2";
            //二级
            TreeNode node11 = new TreeNode();
            node11.ImageIndex = 1;  //based 0
            node11.Text = "缓冲区";
            TreeNode node12 = new TreeNode();
            node12.Text = "12";
            TreeNode node21 = new TreeNode();
            node21.Text = "21";
            TreeNode node22 = new TreeNode();
            node22.Text = "22";
            //二级加入一级
            node1.Nodes.Add(node11);
            node1.Nodes.Add(node12);
            node2.Nodes.Add(node21);
            node2.Nodes.Add(node22);
            //一级加入根
            root.Nodes.Add(node1);
            root.Nodes.Add(node2);
            //
            treeView1.Nodes.Add(root);


        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            //MessageBox.Show("hh");
            SpatialAnalysis sa = new SpatialAnalysis();
            IGeometry g = sa.buffer(Global.ifeature, 100);

            Datamanagement dm = new Datamanagement();
            dm.showGraphics(g);
        }
    }
}
