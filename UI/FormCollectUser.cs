﻿using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RFIDentify.UI
{
	public partial class FormCollectUser : UIPage
	{
		//EChart e = new();
		public FormCollectUser()
		{
			InitializeComponent();
			//Controls.Add(e);
			eChart1.EnableSaveButton();
		}
	}
}