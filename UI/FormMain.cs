using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;

namespace RFIDentify.UI
{
    public partial class FormMain : UIAsideMainFrame
    {
        public FormMain()
        {
            InitializeComponent();
            Aside.TabControl = MainTabControl;

            int pageindex = 1000;

            AddPage(new FormIdentify(), pageindex);
            Aside.CreateNode("识别", pageindex++);

            AddPage(new FormUsers(), pageindex);
            Aside.CreateNode("认证", pageindex++);

            AddPage(new FormRegister(18), pageindex);
            Aside.CreateNode("其它", pageindex++);

        }
    }
}
