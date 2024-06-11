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
			eChart1 = new EChart();
			SuspendLayout();
			// 
			// eChart1
			// 
			eChart1.AutoSize = true;
			eChart1.Location = new Point(46, 89);
			eChart1.Name = "eChart1";
			eChart1.Size = new Size(956, 589);
			eChart1.TabIndex = 0;
			eChart1.WriteCsvFilePath = "CollectionData/Data/temp.csv";
			// 
			// FormCollectUser
			// 
			AutoScaleMode = AutoScaleMode.None;
			ClientSize = new Size(1030, 700);
			Controls.Add(eChart1);
			Name = "FormCollectUser";
			Text = "FormCollectUser";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private EChart eChart1;
	}
}