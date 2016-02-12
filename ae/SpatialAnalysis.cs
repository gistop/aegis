using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ae
{
    class SpatialAnalysis
    {
        //缓冲区
        public IGeometry buffer(IFeature pFeature, double dis)
        {
            ITopologicalOperator topolOperator = pFeature.Shape as ITopologicalOperator;
            IGeometry bufferGeometry = topolOperator.Buffer(dis);
            return bufferGeometry;
        }
        //缓冲区end
    }
}
