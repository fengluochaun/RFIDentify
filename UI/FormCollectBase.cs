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
	public partial class FormCollectBase : UIPage
	{
		private FormMain parent;

		private readonly string basePath = AppDomain.CurrentDomain.BaseDirectory;
		private readonly string defaultBaseStandPath = "CollectionData\\Base\\baseStand.csv";
		private string? writeCsvFilePath = $"CollectionData\\Base\\baseStand{DateTime.Now:yyyyMMddHHmmss}.csv";
		private List<string> baseStandPathList = new List<string>();

		public readonly HttpHelper PhttpHelper = new("http://127.0.0.1:5000/");

		public FormCollectBase(FormMain parent)
		{
			InitializeComponent();
			eChart.EnableSaveButton();
			eChart.AccessibilityObject.Name = "采集基准信息";
			eChart.OnSave += btn_SaveFile_Click;
			eChart.IsProcessed = false;
			eChart.WriteCsvFilePath = Path.Combine(basePath, writeCsvFilePath);

			baseStandPathList = ConfigManager.GetStringListFromConfig("BaseStandPathList");
			if (baseStandPathList.Count == 0)
			{
				baseStandPathList.Add(basePath + defaultBaseStandPath);
				ConfigManager.SaveStringListToConfig("BaseStandPathList", baseStandPathList);
				ConfigManager.SaveValueToConfig("CurrentBaseStandPath", "0");
			}
			
			this.parent = parent;
		}

		private void btn_SaveFile_Click()
		{
			using SaveFileDialog saveFileDialog = new();
			saveFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
			saveFileDialog.DefaultExt = "csv";
			saveFileDialog.Title = "Save CSV File";

			// 显示对话框并检查用户选择
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				string filePath = saveFileDialog.FileName;
				try
				{
					// 模拟写入数据到文件
					var o = new{
						sourcePath = eChart.WriteCsvFilePath,
						destinationPath = filePath
					};
					Task task = new(async () =>
					{
						var result = await PhttpHelper.PostAsync<object>("Data/GetBase", o);
					});
					task.Start();
					task.Wait();
					baseStandPathList.Add(filePath);
					ConfigManager.SaveStringListToConfig("BaseStandPathList", baseStandPathList);
					ConfigManager.SaveValueToConfig("CurrentBaseStandPath", (baseStandPathList.Count - 1).ToString());
					// 显示保存成功的消息
					//MessageBox.Show("File saved successfully at: " + filePath, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				catch (Exception ex)
				{
					// 处理文件保存过程中可能发生的异常
					MessageBox.Show("An error occurred while saving the file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	}
}
