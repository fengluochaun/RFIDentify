namespace RFIDentify.UI
{
	partial class FormCollectUser
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			lbl_Id = new Sunny.UI.UILabel();
			comboBox = new Sunny.UI.UIComboBox();
			uiLabel1 = new Sunny.UI.UILabel();
			btn_SelectFile = new Sunny.UI.UISymbolButton();
			eChart = new EChart();
			uiToolTip = new Sunny.UI.UIToolTip(components);
			SuspendLayout();
			// 
			// lbl_Id
			// 
			lbl_Id.Font = new Font("宋体", 14F, FontStyle.Regular, GraphicsUnit.Point);
			lbl_Id.ForeColor = Color.FromArgb(48, 48, 48);
			lbl_Id.Location = new Point(53, 52);
			lbl_Id.Name = "lbl_Id";
			lbl_Id.Size = new Size(125, 36);
			lbl_Id.Style = Sunny.UI.UIStyle.Custom;
			lbl_Id.TabIndex = 6;
			lbl_Id.Text = "编号：";
			lbl_Id.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// comboBox
			// 
			comboBox.DataSource = null;
			comboBox.FillColor = Color.White;
			comboBox.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
			comboBox.ItemHoverColor = Color.FromArgb(155, 200, 255);
			comboBox.ItemSelectForeColor = Color.FromArgb(235, 243, 255);
			comboBox.Location = new Point(513, 52);
			comboBox.Margin = new Padding(4, 5, 4, 5);
			comboBox.MinimumSize = new Size(63, 0);
			comboBox.Name = "comboBox";
			comboBox.Padding = new Padding(0, 0, 30, 2);
			comboBox.RectColor = SystemColors.ControlDark;
			comboBox.Size = new Size(266, 36);
			comboBox.TabIndex = 7;
			comboBox.Text = "选择基准值文件";
			comboBox.TextAlignment = ContentAlignment.MiddleLeft;
			comboBox.Watermark = "";
			comboBox.SelectedIndexChanged += comboBox_SelectedIndexChanged;
			// 
			// uiLabel1
			// 
			uiLabel1.Font = new Font("宋体", 14F, FontStyle.Regular, GraphicsUnit.Point);
			uiLabel1.ForeColor = Color.FromArgb(48, 48, 48);
			uiLabel1.Location = new Point(351, 52);
			uiLabel1.Name = "uiLabel1";
			uiLabel1.Size = new Size(155, 36);
			uiLabel1.Style = Sunny.UI.UIStyle.Custom;
			uiLabel1.TabIndex = 6;
			uiLabel1.Text = "基准值文件：";
			uiLabel1.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// btn_SelectFile
			// 
			btn_SelectFile.FillColor = SystemColors.Control;
			btn_SelectFile.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
			btn_SelectFile.ForeColor = Color.Black;
			btn_SelectFile.Location = new Point(786, 52);
			btn_SelectFile.MinimumSize = new Size(1, 1);
			btn_SelectFile.Name = "btn_SelectFile";
			btn_SelectFile.RectColor = SystemColors.ControlDark;
			btn_SelectFile.Size = new Size(132, 36);
			btn_SelectFile.Symbol = 61717;
			btn_SelectFile.SymbolColor = Color.Black;
			btn_SelectFile.TabIndex = 8;
			btn_SelectFile.Text = "选择文件";
			btn_SelectFile.Click += btn_SelectFile_Click;
			// 
			// eChart
			// 
			eChart.BaseStandPath = "\\\\?\\C:\\Users\\feng\\AppData\\Local\\Microsoft\\VisualStudio\\17.0_6fd65f15\\WinFormsDesigner\\l4dh0ro5.w4r\\CollectionData/Base/baseStand.csv";
			eChart.Location = new Point(44, 120);
			eChart.Name = "eChart";
			eChart.Size = new Size(939, 582);
			eChart.TabIndex = 9;
			eChart.WriteCsvFilePath = null;
			// 
			// uiToolTip
			// 
			uiToolTip.BackColor = Color.FromArgb(54, 54, 54);
			uiToolTip.ForeColor = Color.FromArgb(239, 239, 239);
			uiToolTip.OwnerDraw = true;
			// 
			// FormCollectUser
			// 
			AutoScaleMode = AutoScaleMode.None;
			ClientSize = new Size(1030, 700);
			Controls.Add(eChart);
			Controls.Add(btn_SelectFile);
			Controls.Add(comboBox);
			Controls.Add(uiLabel1);
			Controls.Add(lbl_Id);
			Name = "FormCollectUser";
			Text = "FormCollectUser";
			ResumeLayout(false);
		}

		#endregion

		private Sunny.UI.UILabel lbl_Id;
		private Sunny.UI.UIComboBox comboBox;
		private Sunny.UI.UILabel uiLabel1;
		private Sunny.UI.UISymbolButton btn_SelectFile;
		private EChart eChart;
		private Sunny.UI.UIToolTip uiToolTip;
	}
}