namespace RFIDentify.UI
{
	partial class FormCollectBase
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
			eChart = new EChart();
			uiToolTip = new Sunny.UI.UIToolTip(components);
			SuspendLayout();
			// 
			// eChart
			// 
			eChart.BaseStandPath = "\\\\?\\C:\\Users\\feng\\AppData\\Local\\Microsoft\\VisualStudio\\17.0_6fd65f15\\WinFormsDesigner\\l4dh0ro5.w4r\\CollectionData/Base/baseStand.csv";
			eChart.Location = new Point(44, 76);
			eChart.Name = "eChart";
			eChart.Size = new Size(940, 585);
			eChart.TabIndex = 9;
			eChart.WriteCsvFilePath = null;
			// 
			// uiToolTip
			// 
			uiToolTip.BackColor = Color.FromArgb(54, 54, 54);
			uiToolTip.ForeColor = Color.FromArgb(239, 239, 239);
			uiToolTip.OwnerDraw = true;
			// 
			// FormCollectBase
			// 
			AutoScaleMode = AutoScaleMode.None;
			ClientSize = new Size(1030, 700);
			Controls.Add(eChart);
			Name = "FormCollectBase";
			Text = "FormCollectUser";
			ResumeLayout(false);
		}

		#endregion
		private EChart eChart;
		private Sunny.UI.UIToolTip uiToolTip;
	}
}