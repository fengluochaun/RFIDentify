namespace RFIDentify.UI
{
	partial class FormIdentifyRecords
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
			DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
			lbl1 = new Sunny.UI.UILabel();
			txt_Search = new Sunny.UI.UITextBox();
			btn_Search = new Sunny.UI.UIButton();
			identificationRecordBindingSource = new BindingSource(components);
			col_RecognitionStatus = new DataGridViewTextBoxColumn();
			col_Time = new DataGridViewTextBoxColumn();
			Col_Name = new DataGridViewTextBoxColumn();
			col_UserId = new DataGridViewTextBoxColumn();
			col_RecordId = new DataGridViewTextBoxColumn();
			DGV_Records = new Sunny.UI.UIDataGridView();
			((System.ComponentModel.ISupportInitialize)identificationRecordBindingSource).BeginInit();
			((System.ComponentModel.ISupportInitialize)DGV_Records).BeginInit();
			SuspendLayout();
			// 
			// lbl1
			// 
			lbl1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
			lbl1.ForeColor = Color.FromArgb(48, 48, 48);
			lbl1.Location = new Point(47, 48);
			lbl1.Name = "lbl1";
			lbl1.Size = new Size(208, 29);
			lbl1.TabIndex = 4;
			lbl1.Text = "用户认证记录表";
			lbl1.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// txt_Search
			// 
			txt_Search.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
			txt_Search.Location = new Point(549, 48);
			txt_Search.Margin = new Padding(4, 5, 4, 5);
			txt_Search.MinimumSize = new Size(1, 16);
			txt_Search.Name = "txt_Search";
			txt_Search.Padding = new Padding(5);
			txt_Search.RectColor = Color.White;
			txt_Search.ShowText = false;
			txt_Search.Size = new Size(251, 36);
			txt_Search.TabIndex = 5;
			txt_Search.Text = "输入关键词";
			txt_Search.TextAlignment = ContentAlignment.MiddleLeft;
			txt_Search.Watermark = "";
			// 
			// btn_Search
			// 
			btn_Search.FillColor = Color.Beige;
			btn_Search.FillColor2 = Color.LightCyan;
			btn_Search.FillColorGradient = true;
			btn_Search.FillColorGradientDirection = FlowDirection.RightToLeft;
			btn_Search.FillHoverColor = Color.LightCyan;
			btn_Search.FillPressColor = Color.Beige;
			btn_Search.FillSelectedColor = Color.Beige;
			btn_Search.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
			btn_Search.ForeColor = Color.Black;
			btn_Search.ForeHoverColor = Color.Black;
			btn_Search.ForePressColor = Color.Black;
			btn_Search.Location = new Point(807, 40);
			btn_Search.MinimumSize = new Size(1, 1);
			btn_Search.Name = "btn_Search";
			btn_Search.Radius = 15;
			btn_Search.RectColor = Color.LightCyan;
			btn_Search.RectHoverColor = Color.Beige;
			btn_Search.RectPressColor = Color.Transparent;
			btn_Search.RectSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Right;
			btn_Search.RectSize = 2;
			btn_Search.Size = new Size(125, 44);
			btn_Search.TabIndex = 13;
			btn_Search.Text = "搜索";
			btn_Search.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point);
			btn_Search.Click += btn_Search_Click;
			// 
			// identificationRecordBindingSource
			// 
			identificationRecordBindingSource.DataSource = typeof(Models.IdentificationRecord);
			// 
			// col_RecognitionStatus
			// 
			col_RecognitionStatus.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			col_RecognitionStatus.DataPropertyName = "RecognitionStatus";
			col_RecognitionStatus.FillWeight = 20F;
			col_RecognitionStatus.HeaderText = "认证状态";
			col_RecognitionStatus.MinimumWidth = 6;
			col_RecognitionStatus.Name = "col_RecognitionStatus";
			// 
			// col_Time
			// 
			col_Time.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			col_Time.DataPropertyName = "Time";
			col_Time.FillWeight = 30F;
			col_Time.HeaderText = "时间";
			col_Time.MinimumWidth = 6;
			col_Time.Name = "col_Time";
			// 
			// Col_Name
			// 
			Col_Name.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			Col_Name.DataPropertyName = "Name";
			Col_Name.FillWeight = 20F;
			Col_Name.HeaderText = "姓名";
			Col_Name.MinimumWidth = 6;
			Col_Name.Name = "Col_Name";
			// 
			// col_UserId
			// 
			col_UserId.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			col_UserId.DataPropertyName = "UserId";
			col_UserId.FillWeight = 15F;
			col_UserId.HeaderText = "用户编号";
			col_UserId.MinimumWidth = 6;
			col_UserId.Name = "col_UserId";
			col_UserId.ReadOnly = true;
			// 
			// col_RecordId
			// 
			col_RecordId.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			col_RecordId.DataPropertyName = "RecordId";
			col_RecordId.FillWeight = 15F;
			col_RecordId.HeaderText = "记录编号";
			col_RecordId.MinimumWidth = 6;
			col_RecordId.Name = "col_RecordId";
			// 
			// DGV_Records
			// 
			dataGridViewCellStyle1.BackColor = Color.FromArgb(235, 243, 255);
			DGV_Records.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			DGV_Records.BackgroundColor = Color.White;
			DGV_Records.BorderStyle = BorderStyle.Fixed3D;
			DGV_Records.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.BackColor = Color.White;
			dataGridViewCellStyle2.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
			dataGridViewCellStyle2.ForeColor = Color.Black;
			dataGridViewCellStyle2.SelectionBackColor = Color.AntiqueWhite;
			dataGridViewCellStyle2.SelectionForeColor = Color.Black;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
			DGV_Records.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			DGV_Records.ColumnHeadersHeight = 50;
			DGV_Records.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
			DGV_Records.Columns.AddRange(new DataGridViewColumn[] { col_RecordId, col_UserId, Col_Name, col_Time, col_RecognitionStatus });
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle3.BackColor = SystemColors.Window;
			dataGridViewCellStyle3.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
			dataGridViewCellStyle3.ForeColor = Color.FromArgb(48, 48, 48);
			dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
			DGV_Records.DefaultCellStyle = dataGridViewCellStyle3;
			DGV_Records.EnableHeadersVisualStyles = false;
			DGV_Records.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
			DGV_Records.GridColor = Color.White;
			DGV_Records.Location = new Point(47, 126);
			DGV_Records.Name = "DGV_Records";
			DGV_Records.RectColor = Color.White;
			DGV_Records.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
			dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle4.BackColor = Color.Snow;
			dataGridViewCellStyle4.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
			dataGridViewCellStyle4.ForeColor = Color.FromArgb(48, 48, 48);
			dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(128, 255, 255);
			dataGridViewCellStyle4.SelectionForeColor = Color.Black;
			dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
			DGV_Records.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
			DGV_Records.RowHeadersWidth = 51;
			dataGridViewCellStyle5.BackColor = Color.White;
			dataGridViewCellStyle5.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
			dataGridViewCellStyle5.ForeColor = Color.Black;
			dataGridViewCellStyle5.SelectionBackColor = Color.OldLace;
			dataGridViewCellStyle5.SelectionForeColor = Color.Black;
			DGV_Records.RowsDefaultCellStyle = dataGridViewCellStyle5;
			DGV_Records.RowTemplate.Height = 40;
			DGV_Records.ScrollBarColor = Color.Silver;
			DGV_Records.ScrollBarRectColor = Color.White;
			DGV_Records.ScrollBarStyleInherited = false;
			DGV_Records.SelectedIndex = -1;
			DGV_Records.Size = new Size(936, 528);
			DGV_Records.StripeOddColor = Color.FromArgb(235, 243, 255);
			DGV_Records.TabIndex = 14;
			// 
			// FormIdentifyRecords
			// 
			AutoScaleMode = AutoScaleMode.None;
			ClientSize = new Size(1030, 700);
			Controls.Add(DGV_Records);
			Controls.Add(btn_Search);
			Controls.Add(txt_Search);
			Controls.Add(lbl1);
			Name = "FormIdentifyRecords";
			Text = "FormIdentifyRecords";
			Load += FormIdentifyRecords_Load;
			((System.ComponentModel.ISupportInitialize)identificationRecordBindingSource).EndInit();
			((System.ComponentModel.ISupportInitialize)DGV_Records).EndInit();
			ResumeLayout(false);
		}

		#endregion
		private Sunny.UI.UILabel lbl1;
		private Sunny.UI.UITextBox txt_Search;
		private	Sunny.UI.UIDataGridView DGV_Records;
		private Sunny.UI.UIButton btn_Search;
		private BindingSource identificationRecordBindingSource;
		private DataGridViewTextBoxColumn col_RecognitionStatus;
		private DataGridViewTextBoxColumn col_Time;
		private DataGridViewTextBoxColumn Col_Name;
		private DataGridViewTextBoxColumn col_UserId;
		private DataGridViewTextBoxColumn col_RecordId;
	}
}