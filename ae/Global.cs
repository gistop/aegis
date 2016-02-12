using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ae
{
    class Global
    {
        public static AxMapControl mainmap;
        public static AxTOCControl toc;
        public static DataGridView dgvattribution;
        public static AxPageLayoutControl plt;
        public static AxToolbarControl maptoolbar;
        public static string keyword;
        public static IFeature ifeature;
    }
}
