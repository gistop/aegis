using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using PropertiesForm;
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

            ToolStripMenuItem itemexportdata = new ToolStripMenuItem();
            itemexportdata.Text = "导出数据";
            itemexportdata.Click += itemexportdata_Click;
            contextmenustrip.Items.Add(itemexportdata);

            ToolStripMenuItem itemopenproperties = new ToolStripMenuItem();
            itemopenproperties.Text = "打开图层属性";
            itemopenproperties.Click += itemopenproperties_Click;
            contextmenustrip.Items.Add(itemopenproperties);






        }

        //导出数据
        void itemexportdata_Click(object sender, EventArgs e)
        {
            //IFeatureLayer GetFeatureLayer();
            IFeatureLayer pFeatureLayer = (IFeatureLayer)Global.mainmap.get_Layer(0);
            IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;


            //调用保存文件函数
            string shpPath;
            SaveFileDialog sa = new SaveFileDialog();
            sa.Filter = "SHP文件(.shp)|*.shp";
            sa.ShowDialog();
            sa.CreatePrompt = true;
            string ExportShapeFileName = sa.FileName;
            // string StrFilter = "SHP文件(.shp)|*.shp";
            // string ExportShapeFileName = SaveFileDialog(StrFilter);
            if (ExportShapeFileName == "")
                return;
            string ExportFileShortName = System.IO.Path.GetFileNameWithoutExtension(ExportShapeFileName);
            string ExportFilePath = System.IO.Path.GetDirectoryName(ExportShapeFileName);
            shpPath = ExportFilePath + "\\" + ExportFileShortName + "\\" + ExportFileShortName + ".shp";
            //设置导出要素类的参数



            try
            {
                //string sFileName = System.IO.Path.GetFileName(fileName);
                //string sFilePath = System.IO.Path.GetDirectoryName(fileName);

                string sFileName = ExportFileShortName + ".shp";
                string sFilePath = System.IO.Path.GetDirectoryName(shpPath);

                IDataset pDataset = pFeatureClass as IDataset;

                IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactoryClass();
                IFeatureWorkspace pFeatureWorkspace = pWorkspaceFactory.OpenFromFile(sFilePath, 0) as IFeatureWorkspace;

                IWorkspace pWorkspace = pFeatureWorkspace as IWorkspace;
                if (pWorkspace.Exists() == true)
                {
                    //DelFeatureFile(sFilePath, sFileName);
                }
                pDataset.Copy(sFileName, pFeatureWorkspace as IWorkspace);

            }
            catch { MessageBox.Show("错误"); }  
        }
        void DelFeatureFile(string sDir, string sName)
        {
            //IFeatureClass pFeatCls = GetFeatureClassByFileName(sDir + sName);
            //if (pFeatCls != null)
            //{
            //    IDataset dataset = pFeatCls as IDataset;
            //    dataset.Delete();
            //}

        }  

        void itemopenproperties_Click(object sender, EventArgs e)
        {
            Form propertiesform = new Form();
            PropertiesUC propertiesuc = new PropertiesUC();
            propertiesuc.ApplyEvents += propertiesuc_ApplyEvents;
            propertiesform.Controls.Add(propertiesuc);
            propertiesform.Show();


        }

        void propertiesuc_ApplyEvents()
        {
            //MessageBox.Show("xxx");
            IFeatureLayer featurelayer = GetFeatureLayer();
            ClassBreaksRenderer(featurelayer);
            Refresh(2);
        }

        //0地图 1toc 2all
        void Refresh(int type)
        {
            IMap pMap = Global.mainmap.Map;
            IActiveView pActiveView;
            pActiveView = pMap as IActiveView;
            pActiveView.Refresh();
            Global.toc.Update();
        }


        IFeatureLayer GetFeatureLayer()
        {
           IFeatureLayer pFeatureLayer = Global.mainmap.get_Layer(0) as IFeatureLayer;
           return pFeatureLayer;
        }

        void ClassBreaksRenderer(IFeatureLayer featureLayer)
        {
            IClassBreaksRenderer classBreaksRenderer = new ClassBreaksRenderer();
            classBreaksRenderer.Field = "AREA";
            // 必须设置分类数量。
            classBreaksRenderer.BreakCount = 4;
            classBreaksRenderer.set_Break(0, 744366000);
            classBreaksRenderer.set_Break(1, 2049800000);
            classBreaksRenderer.set_Break(2, 3801580000);

            // 设置填充样式。
            IFillSymbol fillSymbol = new SimpleFillSymbol();
            fillSymbol.Color = new RgbColor() { Red = 255, Green = 0, Blue = 0 };
            classBreaksRenderer.set_Symbol(0, (ISymbol)fillSymbol);
            fillSymbol = new SimpleFillSymbol();
            fillSymbol.Color = new RgbColor() { Red = 0, Green = 255, Blue = 0 };
            classBreaksRenderer.set_Symbol(1, (ISymbol)fillSymbol);
            fillSymbol = new SimpleFillSymbol();
            fillSymbol.Color = new RgbColor() { Red = 0, Green = 0, Blue = 255 };
            classBreaksRenderer.set_Symbol(2, (ISymbol)fillSymbol);

            // 图层设置渲染。
            var featureRenderer = (IFeatureRenderer)classBreaksRenderer;
            var geoFeatureLayer = (IGeoFeatureLayer)featureLayer;
            geoFeatureLayer.Renderer = featureRenderer;
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

        //添加字段
        public void addField(string fieldname,string type)
        {
            //new a field and add to the first layer in the map
            //new a field: "name_cit", type:string
            IField pField = new FieldClass();
            IFieldEdit pFieldEdit = pField as IFieldEdit;
            pFieldEdit.Name_2 = "name_city";
            pFieldEdit.Type_2 = esriFieldType.esriFieldTypeString;
            //achieve the first layer in the map
            IFeatureLayer pFeatureLayer = Global.mainmap.Map.get_Layer(0) as IFeatureLayer;
            IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
            IClass pTable = pFeatureClass as IClass;        //use ITable or IClass
            pTable.AddField(pFieldEdit);
            //set values of every feature's field-"name_cit" in the first layer
            for (int i = 0; i < pFeatureClass.FeatureCount(null); i++)
            {
                IFeature pFeature = pFeatureClass.GetFeature(i);
                pFeature.set_Value(pFeature.Fields.FindField("name_city"), "city_name");
                pFeature.Store();
            }
        }
        //添加字段end


        public void connectSDE()
        {
            ESRI.ArcGIS.esriSystem.IPropertySet propertySet = new PropertySetClass();
            propertySet.SetProperty("server", "42.96.131.137");
            propertySet.SetProperty("instance", "sde:oracle11g:orcl");//这个隐藏的很深
            propertySet.SetProperty("database", "orcl");//数据库名称
            propertySet.SetProperty("user", "sys");
            propertySet.SetProperty("password", "iswift638187");
            propertySet.SetProperty("version", "SDE.DEFAULT");
            SdeWorkspaceFactory sdeWkspFact = new SdeWorkspaceFactoryClass();
            IFeatureWorkspace pFeaWksp = (IFeatureWorkspace)sdeWkspFact.Open(propertySet, 0);

            IFeatureClass pFCRoads = pFeaWksp.OpenFeatureClass("point");
            IFeatureLayer pFLRoads = new FeatureLayer();
            pFLRoads.FeatureClass = pFCRoads;
            pFLRoads.Name = "点";
            ILayer pLayerRoads = pFLRoads as ILayer;
            Global.mainmap.AddLayer(pLayerRoads);
        }

    }
}
