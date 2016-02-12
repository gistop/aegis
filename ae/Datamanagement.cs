﻿using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
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

        public void copyToPageLayout()
        {
            IObjectCopy objectcopy = new ObjectCopyClass();
            object copyFromMap = Global.mainmap.Map;
            object copyMap = objectcopy.Copy(copyFromMap);
            object copyToMap = Global.plt.ActiveView.FocusMap;
            objectcopy.Overwrite(copyMap, ref copyToMap);
        }

        //显示临时数据
        public void showGraphics(IGeometry pBufferGeo)
        {
            IGraphicsContainer pGraphicsContainer = Global.mainmap.Map as IGraphicsContainer;    //定义容器
            //pFeature = pEnumFeature.Next();     //遍历要素
            //if (pFeature == null)            //若不存在要素，则推出循环
            //    break;
            //pGeometry = pFeature.Shape;     //获取要素的Geometry
            //ITopologicalOperator pTopoOperator = pGeometry as ITopologicalOperator; //QI到拓扑操作
            //IGeometry pBufferGeo = pTopoOperator.Buffer(2);     //缓冲区分析

            IElement pElement = new PolygonElement();
            pElement.Geometry = pBufferGeo;     //获取得到的缓冲区

            pGraphicsContainer.AddElement(pElement, 0); //显示缓冲区
            //Global.mainmap
            IMap pMap = Global.mainmap.Map;
            IActiveView pActiveView;
            pActiveView = pMap as IActiveView;
            pActiveView.Refresh();
        }
        //显示临时数据end

    }
}
