using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
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
    public partial class MapForm : DockContent
    {
        public MapForm()
        {
            InitializeComponent();
            Global.toc.SetBuddyControl(this.axMapControl1);
            Global.mainmap = this.axMapControl1;
        }

        private void axMapControl1_OnAfterScreenDraw(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnAfterScreenDrawEvent e)
        {
            Datamanagement dt = new Datamanagement();
            dt.copyToPageLayout();
        }

        private void axMapControl1_OnMapReplaced(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMapReplacedEvent e)
        {
            if (this.axMapControl1.LayerCount > 0)
            {
                Global.eagleeye.Map = new MapClass();
                for (int i = 0; i <= (axMapControl1.Map.LayerCount - 1); i++)
                {
                    Global.eagleeye.AddLayer(axMapControl1.get_Layer(i));
                }
                Global.eagleeye.Extent = axMapControl1.Extent;
                Global.eagleeye.Refresh();
            }
        }

        private void axMapControl1_OnExtentUpdated(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnExtentUpdatedEvent e)
        {
            //得到新范围
            IEnvelope pEnvelop = (IEnvelope)e.newEnvelope;
            IGraphicsContainer pGraphicsContainer = Global.eagleeye.Map as IGraphicsContainer;
            IActiveView pActiveView = pGraphicsContainer as IActiveView;
            //在绘制前。清除axMapControl2 中的任何图形元素
            pGraphicsContainer.DeleteAllElements();
            IRectangleElement pRectangleEle = new RectangleElementClass();
            IElement pElement = pRectangleEle as IElement;
            pElement.Geometry = pEnvelop;
            //设置鹰眼中的红线框
            IRgbColor pColor = new RgbColorClass();
            pColor.Red = 255;
            pColor.Green = 0;
            pColor.Blue = 0;
            pColor.Transparency = 255;
            //产生一个线符号对象
            ILineSymbol pOutline = new SimpleLineSymbolClass();
            pOutline.Width = 1;
            pOutline.Color = pColor;
            //设置颜色属性
            pColor = new RgbColorClass();
            pColor.Red = 255;
            pColor.Green = 0;
            pColor.Blue = 0;
            pColor.Transparency = 0;
            //设置填充符号的属性
            IFillSymbol pFillSymbol = new SimpleFillSymbolClass();
            pFillSymbol.Color = pColor;
            pFillSymbol.Outline = pOutline;
            IFillShapeElement pFillShapeEle = pElement as IFillShapeElement;
            pFillShapeEle.Symbol = pFillSymbol;
            pGraphicsContainer.AddElement((IElement)pFillShapeEle, 0);
            pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }
    }
}
