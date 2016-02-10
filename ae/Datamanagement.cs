using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ae
{
    class Datamanagement
    {
        public void addContextmenu(ContextMenuStrip contextmenustrip)
        {
            ToolStripMenuItem itemopenattribution = new ToolStripMenuItem();
            itemopenattribution.Text = "打开属性表";
            itemopenattribution.Click += itemopenattribution_Click;
            contextmenustrip.Items.Add(itemopenattribution);
        }

        void itemopenattribution_Click(object sender, EventArgs e)
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
