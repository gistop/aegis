using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ae
{
    class Thematicmap
    {
        /// <summary>
        /// 简单渲染
        /// </summary>
        /// <param name="layerName">图层名字</param>
        /// <param name="FillStyle">FillStyle</param>
        /// <param name="pColor">FillColor</param>
        /// <param name="OutLineColor">OutLineColor</param>
        /// <param name="RenderLabel">样式名称注释</param>
        /// <param name="Descripition">描述</param>
        public void createSimpleFillSymbol(string layerName, esriSimpleFillStyle FillStyle, IColor pColor, IColor OutLineColor, string RenderLabel, string Descripition)
        {
            //简单填充符号
            ISimpleFillSymbol simpleFillSymbol = new SimpleFillSymbolClass();
            //可以用符号选择器进行
            simpleFillSymbol.Style = FillStyle;
            simpleFillSymbol.Color = pColor;
            //创建边线符号
            ISimpleLineSymbol simpleLineSymbol = new SimpleLineSymbolClass();
            simpleLineSymbol.Style = esriSimpleLineStyle.esriSLSSolid;
            simpleLineSymbol.Color = OutLineColor;
            ISymbol symbol = simpleLineSymbol as ISymbol;
            symbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
            simpleFillSymbol.Outline = simpleLineSymbol;

            ISimpleRenderer simpleRender = new SimpleRendererClass();
            simpleRender.Symbol = simpleFillSymbol as ISymbol;
            simpleRender.Label = RenderLabel;
            simpleRender.Description = Descripition;

            IGeoFeatureLayer geoFeatureLayer;
            geoFeatureLayer = getGeoLayer(layerName);
            if (geoFeatureLayer != null)
            {
                geoFeatureLayer.Renderer = simpleRender as IFeatureRenderer;
            }
        }


        private IGeoFeatureLayer getGeoLayer(string layerName)
        {
            ILayer layer;
            IGeoFeatureLayer geoFeatureLayer;
            for (int i = 0; i < Global.mainmap.LayerCount; i++)
            {
                layer = Global.mainmap.get_Layer(i);
                if (layer != null && layer.Name == layerName)
                {
                    geoFeatureLayer = layer as IGeoFeatureLayer;
                    return geoFeatureLayer;
                }
            }
            return null;
        }

    }
}
