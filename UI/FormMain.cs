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
        public FormIdentify formIdentify;
        public FormUsers formUsers;
        public FormRegister formRegister;
        public FormMain()
        {
            InitializeComponent();

            formIdentify = new FormIdentify(this);
            formUsers = new FormUsers();
            formRegister = new FormRegister();

            Aside.TabControl = MainTabControl;

            int pageindex = 1000;

            

            AddPage(formIdentify, pageindex);
            Aside.CreateNode("识别", pageindex++);

            AddPage(formUsers, pageindex);
            Aside.CreateNode("认证", pageindex++);

            AddPage(formRegister, pageindex);
            Aside.CreateNode("其它", pageindex++);

        }
        
        public void test()
        {
            int a = 0;
            a = a + 1;
        }
        

    }
}
