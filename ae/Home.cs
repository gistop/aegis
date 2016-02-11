using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
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
                    SpatialQuery();
                    break;
                default:

                    break;
            }


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
        private void SpatialQuery()
        { 
        }
        //空间查询end


    }
}
