#define SIMULATION

using RFIDentify.Com;
using Sunny.UI;
using System.Collections.Concurrent;
using Timer = System.Windows.Forms.Timer;

namespace RFIDentify.UI
{
	public partial class FormIdentify : UIPage
	{
		private FormMain parent;
		private readonly HttpHelper PhttpHelper = new("http://127.0.0.1:5000/");

		private string? writeCsvFilePath;
		private List<string> baseStandPathList = new List<string>();
		private readonly string basePath = AppDomain.CurrentDomain.BaseDirectory;
		private readonly string IdentificationPath_ = $"CollectionData\\Identification\\temp{DateTime.Now:yyyyMMddHHmmss}.csv";//识别数据存储路径
		private readonly string defaultBaseStandPath = "CollectionData\\Base\\baseStand.csv";
		public FormIdentify(FormMain parent)
		{
			InitializeComponent();
			this.parent = parent;

			eChart.EnableSaveButton();
			eChart.AccessibilityObject.Name = "采集人员信息";
			eChart.WriteCsvFilePath = basePath + IdentificationPath_;

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
			catch (FileNotFoundException ex)
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

		private void btn_Stop_Click(object sender, EventArgs e)
		{
			// 上传识别数据
			var o = new
			{
				filePath = basePath + IdentificationPath_,
				baseStandPath = basePath + ConfigManager.GetStringFromConfig("CurrentBaseStandPath")
			};
			Task task = new(async () =>
			{
				var result = await PhttpHelper.PostAsync<object>("User/UserRecognition", o);
				MethodInvoker mi = new MethodInvoker(() =>
				{
					this.lbl_Identification.Text = "识别对象：" + result.ToString();
				});
				this.BeginInvoke(mi);
			});
			task.Start();
		}

		private void lbl_Identifcation_Click(object sender, EventArgs e)
		{
			if (int.TryParse((lbl_Identification.Text.SplitLast("：")), out int id))
			{
				parent.formRegister.UpdateByUserId(id);
				parent.SelectPage(1002);
			}
		}
	}
}
