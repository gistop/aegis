using DevComponents.DotNetBar;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ae
{
    class Home
    {
        //开始操作
        public void HomeOperation(ButtonItem button)
        {
            string action = button.Text;
            switch (action)
            {
                case "openmxd":
                    OpenMXD();
                    break;
                case "zoomin":
                    MapNavigation("zoomin");
                    break;
                case "点":
                    SpatialQuery(action);
                    break;
                case "attributequery":
                    AttributeQuery();
                    break;
                case "adddata":
                    AddData();
                    break;
                default:

                    break;
            }


        }

        private void AddData()
        {
            //sde加载
            //Datamanagement dm = new Datamanagement();
            //dm.connectSDE();
            //
            //图片数据加载
            //AddPic();
            //
            //AddShp();
            //AddGDB();
            AddMDB();

        }

        private void AddMDB()
        {
            string filePath = "C:\\temp\\mdb.mdb";
            AccessWorkspaceFactory fac = new AccessWorkspaceFactoryClass();
            IFeatureWorkspace space = (IFeatureWorkspace)fac.OpenFromFile(filePath, 0);
            IFeatureClass featureclass = space.OpenFeatureClass("layer");
            IFeatureLayer pFLRoads = new FeatureLayer();
            pFLRoads.FeatureClass = featureclass;
            pFLRoads.Name = "点";
            ILayer pLayerRoads = pFLRoads as ILayer;
            Global.mainmap.AddLayer(pLayerRoads);
        }

        private void AddGDB()
        {
            FileGDBWorkspaceFactory pWSF = new FileGDBWorkspaceFactory();
            IFeatureWorkspace pWS1 = pWSF.OpenFromFile("C:\\temp\\gdb.gdb", 0) as IFeatureWorkspace;
            IFeatureClass featureclass = pWS1.OpenFeatureClass("layer");
            IFeatureLayer pFLRoads = new FeatureLayer();
            pFLRoads.FeatureClass = featureclass;
            pFLRoads.Name = "点";
            ILayer pLayerRoads = pFLRoads as ILayer;
            Global.mainmap.AddLayer(pLayerRoads);
        }

        private void AddShp()
        {
            System.Windows.Forms.OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "打开工程";
            openFileDialog1.Filter = "*.mxd|*.mxd|*.shp|*.shp";
            openFileDialog1.Multiselect = true;
            openFileDialog1.ShowDialog();
            string filePath = openFileDialog1.FileName;
            if (1 == openFileDialog1.FilterIndex)
            {
                //if (axMapControl1.CheckMxFile(filePath))
                //{
                //    axMapControl1.MousePointer = esriControlsMousePointer.esriPointerArrowHourglass;
                //    axMapControl1.LoadMxFile(filePath, 0, Type.Missing);
                //    axMapControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
                //}
                //else
                //{
                //    MessageBox.Show(filePath + "不是有效的地图文档");
                //}
            }
            else if (2 == openFileDialog1.FilterIndex)
            {
                string[] filepaths = openFileDialog1.FileNames;

                foreach (string file in filepaths)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    string path = file.Substring(0, file.Length - fileInfo.Name.Length);
                    try
                    {
                        Global.mainmap.AddShapeFile(path, fileInfo.Name);
                    }
                    catch (Exception r)
                    {
                        MessageBox.Show("添加图层失败" + r.ToString());
                    }
                }

            }
        }

        private void AddPic()
        {
            System.Windows.Forms.OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.bmp|*.bmp|*.jpg|*.jpg|*.tif|*.tif";
            ofd.ShowDialog();




            string filePath = ofd.FileName;

            //此两个路径和文件名作为参数：

            string pathName = System.IO.Path.GetDirectoryName(filePath);
            string fileName = System.IO.Path.GetFileName(filePath);



            //定义工作空间工厂并实例化：

            IWorkspaceFactory pWSF;
            pWSF = new RasterWorkspaceFactoryClass();

            //

            IWorkspace pWS;
            pWS = pWSF.OpenFromFile(pathName, 0);

            IRasterWorkspace pRWS;
            pRWS = pWS as IRasterWorkspace;


            IRasterDataset pRasterDataset;
            pRasterDataset = pRWS.OpenRasterDataset(fileName);

            //影像金字塔的判断与创建
            IRasterPyramid pRasPyrmid;
            pRasPyrmid = pRasterDataset as IRasterPyramid;

            if (pRasPyrmid != null)
            {
                if (!(pRasPyrmid.Present))
                {
                    pRasPyrmid.Create();
                }
            }

            IRaster pRaster;
            pRaster = pRasterDataset.CreateDefaultRaster();

            IRasterLayer pRasterLayer;
            pRasterLayer = new RasterLayerClass();
            pRasterLayer.CreateFromRaster(pRaster);

            ILayer pLayer = pRasterLayer as ILayer;
            Global.mainmap.AddLayer(pLayer, 0);
        }
        ////开始操作end

        //加载MXD数据
        private void OpenMXD()
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Title = "Open Map Document";
            dlgOpen.Filter = "ArcMap Document(*.mxd)|*.mxd";
            string sFilePath = null;
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                sFilePath = dlgOpen.FileName;
            }
            else
                return;
            if (sFilePath != null)
            {
                if (Global.mainmap.CheckMxFile(sFilePath))
                {
                    Global.mainmap.LoadMxFile(sFilePath);
                }
            }
        }
        //加载MXD数据end

        //地图导航
        private void MapNavigation(string type)
        {
            int index = 0;
            switch (type)
            {
                case "zoomin":
                    index = 1;
                    break;
                case "zoomout":
                    //statements
                    break;
                //...
                default:
                    //statements
                    break;
            }

            ESRI.ArcGIS.SystemUI.ICommand command = Global.maptoolbar.CommandPool.get_Command(index);  //放大按钮索引为3
            string str = command.Name;

            ESRI.ArcGIS.SystemUI.ICommand pCommand;
            pCommand = new ESRI.ArcGIS.Controls.ControlsMapZoomInToolClass();
            pCommand.OnCreate(Global.mainmap.Object);
            Global.mainmap.CurrentTool = pCommand as ESRI.ArcGIS.SystemUI.ITool;


            command.OnClick();
        }
        //地图导航end

        //空间查询
        private void SpatialQuery(string type)
        {
            switch (type)
            {
                case "点":
                    PointQuery();
                    break;
                case "线":
                    //statements
                    break;
                default:
                    //statements
                    break;
            }
        }
        //空间查询end

        //点查询
        private void PointQuery()
        {
            Global.mainmap.OnMouseDown += mainmap_OnMouseDown;
        }

        void mainmap_OnMouseDown(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseDownEvent e)
        {
            int m_px = e.x;
            int m_py = e.y;
            IMap m_pMap = Global.mainmap.Map;
            ClickSelectFeature(ref m_pMap, m_px, m_py);
        }

        private void ClickSelectFeature(ref IMap m_pMap, int x, int y)
        {
            // get the layer
            IFeatureLayer pFeatureLayer = m_pMap.get_Layer(0) as IFeatureLayer;
            if (pFeatureLayer == null) return;
            IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;//get the feature
            if (pFeatureClass == null) return;
            //get mouse position
            IActiveView pActiveView = m_pMap as IActiveView;
            IPoint pPoint = pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            //Use a 4 pixel buffer around the cursor for feature search
            double length;
            length = ConvertPixelsToMapUnits(pActiveView, 4);
            ITopologicalOperator pTopo = pPoint as ITopologicalOperator;
            IGeometry pBuffer = pTopo.Buffer(length);//建立4个地图单位的缓冲区
            IGeometry pGeometry = pBuffer.Envelope;//确定鼠标周围隐藏的选择框

            //新建一个空间约束器
            ISpatialFilter pSpatialFilter;
            IQueryFilter pFilter;
            //设置查询约束条件
            pSpatialFilter = new SpatialFilter();
            pSpatialFilter.Geometry = pGeometry;

            switch (pFeatureClass.ShapeType)
            {
                case esriGeometryType.esriGeometryPoint:
                    pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelCrosses;
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                    break;
                default:
                    break;
            }
            pSpatialFilter.GeometryField = pFeatureClass.ShapeFieldName;
            pFilter = pSpatialFilter;
            //Do the Search 从图层中查询出满足约束条件的元素
            IFeatureCursor pCursor = pFeatureLayer.Search(pFilter, false);

            //select
            IFeature pFeature = pCursor.NextFeature();
            m_pMap.SelectFeature(pFeatureLayer, pFeature);
            while (pFeature != null)
            {
                m_pMap.SelectFeature(pFeatureLayer, pFeature);
                pFeature = pCursor.NextFeature();
            }

            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
        }
        private double ConvertPixelsToMapUnits(IActiveView pActiveView, double pixelUnits)
        {
            // Uses the ratio of the size of the map in pixels to map units to do the conversion
            IPoint p1 = pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.UpperLeft;
            IPoint p2 = pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.UpperRight;
            int x1, x2, y1, y2;
            pActiveView.ScreenDisplay.DisplayTransformation.FromMapPoint(p1, out x1, out y1);
            pActiveView.ScreenDisplay.DisplayTransformation.FromMapPoint(p2, out x2, out y2);
            double pixelExtent = x2 - x1;
            double realWorldDisplayExtent = pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.Width;
            double sizeOfOnePixel = realWorldDisplayExtent / pixelExtent;
            return pixelUnits * sizeOfOnePixel;
        }
        //点查询end

        //属性查询
        private IFeatureLayer mFeatureLayer;
        private void AttributeQuery()
        {
            //定义图层，要素游标，查询过滤器，要素
            IFeatureCursor pFeatureCursor;
            IQueryFilter pQueryFilter;
            IFeature pFeature;
            IPoint pPoint;
            IEnvelope pEnv;
            pEnv = Global.mainmap.ActiveView.Extent;
            pPoint = new PointClass();

            pPoint.X = pEnv.XMin + pEnv.Width / 2;
            pPoint.Y = pEnv.YMin + pEnv.Height / 2;
            if (Global.mainmap.LayerCount <= 0)
                return;
            //获取图层
            mFeatureLayer = Global.mainmap.get_Layer(0) as IFeatureLayer;
            //清除上次查询结果
            Global.mainmap.Map.ClearSelection();
            Global.mainmap.ActiveView.Refresh();
            //pQueryFilter的实例化
            pQueryFilter = new QueryFilterClass();
            //设置查询过滤条件
            pQueryFilter.WhereClause = "NAME" + "='" + Global.keyword + "'";
            //查询
            pFeatureCursor = mFeatureLayer.Search(pQueryFilter, true);
            //获取查询到的要素
            pFeature = pFeatureCursor.NextFeature();

            Global.ifeature = pFeature;

            //判断是否获取到要素
            if (pFeature != null)
            {
                //选择要素
                Global.mainmap.Map.SelectFeature(mFeatureLayer, pFeature);
                //放大到要素

                pFeature.Shape.Envelope.CenterAt(pPoint);
                Global.mainmap.Extent = pFeature.Shape.Envelope;

            }
            else
            {
                //没有得到pFeature的提示
                MessageBox.Show("没有找到相关要素！", "提示");
            }
        }
        //属性查询end

    }
}
