using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
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
    public partial class LeftForm : DockContent
    {
        //为了配合toc右键菜单,初始化ITocControl接口方法中HitTest的各个参数
        private esriTOCControlItem pTocItem = esriTOCControlItem.esriTOCControlItemNone;
        private IBasicMap pMap = null;
        private ILayer pLayer = null;
        private object pother = null;
        private object pindex = null;
        
        
        public LeftForm()
        {
            InitializeComponent();
            Global.toc = this.axTOCControl1;
            splitContainer1.SplitterDistance = 10;
        }

        private void axTOCControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.ITOCControlEvents_OnMouseDownEvent e)
        {
            if (e.button == 2)
            {
                //判断所选菜单的类型
                axTOCControl1.HitTest(e.x, e.y, ref pTocItem, ref pMap, ref pLayer, ref pother, ref pindex);
                //axMapControl1.CustomProperty = layer;

                //弹出右键菜单
                /*if (item == esriTOCControlItem.esriTOCControlItemMap)
                    pMenuMap.PopupMenu(e.x, e.y, m_tocControl.hWnd);
                if (item == esriTOCControlItem.esriTOCControlItemLayer)
                    pMenuLayer.PopupMenu(e.x, e.y, m_tocControl.hWnd);*/
                //菜单的创建
                //pMenuMap = new ToolbarMenuClass();
                //pMenuLayer = new ContextMenuStrip();
                if (pTocItem == esriTOCControlItem.esriTOCControlItemLayer)
                {
                    //pFLayer = pLayer as IFeatureLayer;
                    //pFC = pFLayer.FeatureClass;

                    //pFeatureLayer = pLayer as IFeatureLayer;
                    /*   pfeatureclass = pFeatureLayer.FeatureClass;
                       SymbolToolStripMenuItem.DropDownItems.Clear();
                       for (int i = 0; i < pfeatureclass.Fields.FieldCount; i++)
                       {
                           SymbolToolStripMenuItem.DropDownItems.Add(pfeatureclass.Fields.get_Field(i).Name, null, new EventHandler(this.symbolshow_Clicked)); }
                
                   */
                    //toolStripMenuItem2.Click +=toolStripMenuItem2_Click;
                    Datamanagement dm = new Datamanagement();
                    dm.addContextmenu(contextMenuStrip1);

                    contextMenuStrip1.Show(axTOCControl1, new System.Drawing.Point(e.x, e.y));

                    //m_menuLayer.AddItem(new frmAttribute(axMapControl1, pLayer), -1, 2, true, esriCommandStyles.esriCommandStyleTextOnly);

                    //m_menuLayer = new ToolbarMenuClass();
                    //m_menuLayer.AddItem(new OpenAttributeTable(pLayer), -1, 0, true, esriCommandStyles.esriCommandStyleTextOnly);
                    //动态添加图层标注的Command到图层右键菜单
                    //m_menuLayer.AddItem(new LabelLayerCmd(pLayer, m_mapControl), -1, 1, false, esriCommandStyles.esriCommandStyleTextOnly);
                    //m_menuLayer.AddItem(new LabelLayerCmd(pLayer), -1, 1, false, esriCommandStyles.esriCommandStyleTextOnly);

                    //弹出图层右键菜单m_mapControl
                    //m_menuLayer.SetHook(m_mapControl);
                    //m_menuLayer.PopupMenu(e.x, e.y, axTOCControl1.hWnd);
                    // 注意顺序不能颠倒
                    //m_menuLayer.Remove(1);
                    //m_menuLayer.Remove(0)
                };
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            IMap pMap = new MapClass();
            IFeatureLayer pFeatureLayer = (IFeatureLayer)Global.mainmap.get_Layer(0);
            IFields pFields = pFeatureLayer.FeatureClass.Fields;
            DataTable pDataTable = new DataTable();
            for (int i = 0; i < pFields.FieldCount; i++)
            {
                string fldName;
                fldName = pFields.get_Field(i).AliasName;
                pDataTable.Columns.Add(fldName);
            }

            IFeatureCursor pFeatureCursor;
            pFeatureCursor = pFeatureLayer.FeatureClass.Search(null, false);
            IFeature pFeature;
            pFeature = pFeatureCursor.NextFeature();
            while (pFeature != null)
            {
                string fldValue = null;
                DataRow dr = pDataTable.NewRow();
                for (int i = 0; i < pFields.FieldCount; i++)
                {
                    string fldName;
                    fldName = pFields.get_Field(i).Name;
                    if (fldName == "Shape")
                    {
                        fldValue = Convert.ToString(pFeature.Shape.GeometryType);
                    }
                    else
                        fldValue = Convert.ToString(pFeature.get_Value(i));
                    dr[i] = fldValue;
                }
                pDataTable.Rows.Add(dr);
                pFeature = pFeatureCursor.NextFeature();
            }
            Global.dgvattribution.DataSource = pDataTable;
        }
    }
}
