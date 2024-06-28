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
        public FormIdentifyRecords formIdentifyRecords;
        public FormUsers formUsers;
        public FormRegister formRegister;
        public FormRegisterFromEquipment formRegisterFromEquipment;
        public FormCollectUser formCollectUser;
        public FormCollectBase formCollectBase;
        public FormMain()
        {
            InitializeComponent();

            formIdentify = new(this);
            formIdentifyRecords = new(this);
            formUsers = new(this);
            formRegister = new(this);
            formRegisterFromEquipment = new(this); 
            formCollectUser = new(this);
            formCollectBase = new(this);

            formRegister.UserChanged += formCollectUser.UpdateByUser;
            formCollectUser.OnSave += formRegister.GenerateUserTrainData;

            Aside.TabControl = MainTabControl;

            int pageindex = 1000;

            TreeNode parent = Aside.CreateNode("识别", pageindex++);
            AddPage(formIdentify, pageindex);
            Aside.CreateChildNode(parent, "识别人员", pageindex++);
            AddPage(formIdentifyRecords, pageindex);
            Aside.CreateChildNode(parent, "识别记录", pageindex++);

            pageindex = 2000;
            AddPage(formUsers, pageindex);
            Aside.CreateNode("人员", pageindex++);

            pageindex = 3000;
            AddPage(formRegister, pageindex);
            Aside.CreateNode("认证", pageindex++);

            pageindex = 4000;
            //AddPage(formRegisterFromEquipment, pageindex);
            parent = Aside.CreateNode("采集", pageindex++);
            AddPage(formCollectUser, pageindex);
            Aside.CreateChildNode(parent, "采集人员信息", pageindex++);
            AddPage(formCollectBase, pageindex);
            Aside.CreateChildNode(parent, "采集基准信息", pageindex++);
        }      
    }
}
