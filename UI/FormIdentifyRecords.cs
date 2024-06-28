using Sunny.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RFIDentify.Models;
using RFIDentify.DAO;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;

namespace RFIDentify.UI
{
	public partial class FormIdentifyRecords : UIPage
	{
		private FormMain parent;
		private SQLiteHelper SQLiteHelper = SQLiteHelper.GetInstance();
		private readonly string tableName = "IdentificationRecords";
		private BindingSource bindingSource = new BindingSource();
		public FormIdentifyRecords(FormMain parent)
		{
			InitializeComponent();
			this.parent = parent;
		}

		private void FormIdentifyRecords_Load(object sender, EventArgs e)
		{
			DGV_Records.DataSource = bindingSource;
			GetData("");
		}

		private async void GetData(string? keyword)
		{
			try
			{
				var list = await SQLiteHelper.Query<IdentificationRecord>(tableName, "");
				if (!string.IsNullOrEmpty(keyword))
				{
					list = list.Where(x => x.RecordId.ToString().Contains(keyword) || x.UserId.ToString().Contains(keyword) || x.RecognitionStatus!.Contains(keyword) || x.Name!.Contains(keyword)).ToList();
				}
				
				bindingSource.DataSource = list;
			}
			catch (SqlException)
			{
				MessageBox.Show("数据库连接错误！");
			}
		}

		private void btn_Search_Click(object sender, EventArgs e)
		{
			string keyword = txt_Search.Text;
			GetData(keyword);
		}
	}
}
