using com.sun.tools.doclets.@internal.toolkit.taglets;
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
using RFIDentify.Com;

namespace RFIDentify.UI
{
	public partial class FormCollectUser : UIPage
	{
		private FormMain parent;

		public Action<string>? OnSave;
		private readonly string basePath = AppDomain.CurrentDomain.BaseDirectory;
		private readonly string defaultBaseStandPath = "CollectionData\\Base\\baseStand.csv";
		private string? writeCsvFilePath;
		private List<string> baseStandPathList = new List<string>();

		public FormCollectUser(FormMain parent)
		{
			this.parent = parent;
			InitializeComponent();
			eChart.EnableSaveButton();
			eChart.AccessibilityObject.Name = "采集人员信息";
			eChart.OnSave += btn_Save_Click;

			baseStandPathList = ConfigManager.GetStringListFromConfig("BaseStandPathList");
			if (baseStandPathList.Count == 0)
			{
				baseStandPathList.Add(basePath + defaultBaseStandPath);
				ConfigManager.SaveStringListToConfig("BaseStandPathList", baseStandPathList);
				ConfigManager.SaveValueToConfig("CurrentBaseStandPath", "0");
			}

			InitializeComboBox();
		}

		private void InitializeComboBox()
		{
			var list = baseStandPathList.Select(x => x.Split('\\').Last()).ToList();
			comboBox.DataSource = list;
			comboBox.SelectedIndex = ConfigManager.GetIntFromConfig("CurrentBaseStandPath");
		}

		private void UpdateComboBox()
		{
			var list = baseStandPathList.Select(x => x.Split('\\').Last()).ToList();
			comboBox.DataSource = list;
			comboBox.SelectedIndex = list.Count - 1;
		}

		public void UpdateByUser(int id)
		{
			lbl_Id.Text = "编号：" + id;
			string path = Path.Combine(basePath, "User", id.ToString(), "unprocessed");
			//获取文件夹文件的数量
			int time = Directory.GetFiles(path).Length;
			writeCsvFilePath = Path.Combine(basePath, "User", id.ToString(), "unprocessed", $"time-{time}.csv");
			eChart.WriteCsvFilePath = writeCsvFilePath;
		}

		private void btn_SelectFile_Click(object sender, EventArgs e)
		{
			string filename = "";
			try
			{
				if (FileEx.OpenDialog(ref filename, "CSV files (*.csv)|*.csv|All files (*.*)|*.*", "csv"))
				{
					UIMessageTip.ShowOk(filename);
				}
			}
			catch(FileNotFoundException ex)
			{
				Console.WriteLine(ex.Message);
			}
			
			if (string.IsNullOrEmpty(filename))
			{
				return;
			}
			baseStandPathList.Add(filename);
			ConfigManager.SaveStringListToConfig("BaseStandPathList", baseStandPathList);
			UpdateComboBox();
		}

		private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
		{
			uiToolTip.SetToolTip(comboBox, baseStandPathList[comboBox.SelectedIndex]);
			ConfigManager.SaveValueToConfig("CurrentBaseStandPath", comboBox.SelectedIndex.ToString());
			DataProcess.UpdateBaseStand(baseStandPathList[comboBox.SelectedIndex]);
		}

		private void btn_Save_Click()
		{
			parent.SelectPage(3000);
			OnSave!.Invoke(writeCsvFilePath!);
		}
	}
}
