﻿using System;
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
    public partial class FormRegisterFromEquipment : UIHeaderMainFrame
    {
        private FormRegister _formRegister;
        public FormRegisterFromEquipment(FormRegister formRegister)
        {
            InitializeComponent();
            _formRegister = formRegister;
        }

    }
}