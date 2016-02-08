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
                case "1":

                    break;
                case "2":

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


    }
}
