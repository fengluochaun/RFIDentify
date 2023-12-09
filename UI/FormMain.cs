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
        public FormRegisterFromEquipment formRegisterFromEquipment;
        public FormMain()
        {
            InitializeComponent();

            formIdentify = new(this);
            formUsers = new(this);
            formRegister = new(this);
            formRegisterFromEquipment = new(this); 

            Aside.TabControl = MainTabControl;

            int pageindex = 1000;

            

            AddPage(formIdentify, pageindex);
            Aside.CreateNode("识别", pageindex++);

            AddPage(formUsers, pageindex);
            Aside.CreateNode("人员", pageindex++);

            AddPage(formRegister, pageindex);
            Aside.CreateNode("认证", pageindex++);

            AddPage(formRegisterFromEquipment, pageindex);
            Aside.CreateNode("采集", pageindex++);

        }      
    }
}
