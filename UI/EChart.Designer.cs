namespace RFIDentify.UI
{
	partial class EChart
	{
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region 组件设计器生成的代码

		/// <summary> 
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			tableLayoutPanel_Tags = new Sunny.UI.UITableLayoutPanel();
			checkBox_Tag5 = new Sunny.UI.UICheckBox();
			checkBox_Tag4 = new Sunny.UI.UICheckBox();
			checkBox_Tag3 = new Sunny.UI.UICheckBox();
			checkBox_Tag2 = new Sunny.UI.UICheckBox();
			checkBox_Tag1 = new Sunny.UI.UICheckBox();
			lineChart = new Sunny.UI.UILineChart();
			btn_Start = new Sunny.UI.UIButton();
			btn_Stop = new Sunny.UI.UIButton();
			tableLayoutPanel_Tags.SuspendLayout();
			SuspendLayout();
			// 
			// tableLayoutPanel_Tags
			// 
			tableLayoutPanel_Tags.ColumnCount = 1;
			tableLayoutPanel_Tags.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel_Tags.Controls.Add(checkBox_Tag5, 0, 4);
			tableLayoutPanel_Tags.Controls.Add(checkBox_Tag4, 0, 3);
			tableLayoutPanel_Tags.Controls.Add(checkBox_Tag3, 0, 2);
			tableLayoutPanel_Tags.Controls.Add(checkBox_Tag2, 0, 1);
			tableLayoutPanel_Tags.Controls.Add(checkBox_Tag1, 0, 0);
			tableLayoutPanel_Tags.Location = new Point(766, 166);
			tableLayoutPanel_Tags.Name = "tableLayoutPanel_Tags";
			tableLayoutPanel_Tags.RowCount = 5;
			tableLayoutPanel_Tags.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
			tableLayoutPanel_Tags.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
			tableLayoutPanel_Tags.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
			tableLayoutPanel_Tags.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
			tableLayoutPanel_Tags.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
			tableLayoutPanel_Tags.Size = new Size(80, 200);
			tableLayoutPanel_Tags.TabIndex = 0;
			tableLayoutPanel_Tags.TagString = null;
			// 
			// checkBox_Tag5
			// 
			checkBox_Tag5.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
			checkBox_Tag5.ForeColor = Color.FromArgb(48, 48, 48);
			checkBox_Tag5.Location = new Point(3, 163);
			checkBox_Tag5.MinimumSize = new Size(1, 1);
			checkBox_Tag5.Name = "checkBox_Tag5";
			checkBox_Tag5.Size = new Size(74, 34);
			checkBox_Tag5.TabIndex = 5;
			checkBox_Tag5.Text = "tag5";
			// 
			// checkBox_Tag4
			// 
			checkBox_Tag4.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
			checkBox_Tag4.ForeColor = Color.FromArgb(48, 48, 48);
			checkBox_Tag4.Location = new Point(3, 123);
			checkBox_Tag4.MinimumSize = new Size(1, 1);
			checkBox_Tag4.Name = "checkBox_Tag4";
			checkBox_Tag4.Size = new Size(74, 34);
			checkBox_Tag4.TabIndex = 4;
			checkBox_Tag4.Text = "tag4";
			// 
			// checkBox_Tag3
			// 
			checkBox_Tag3.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
			checkBox_Tag3.ForeColor = Color.FromArgb(48, 48, 48);
			checkBox_Tag3.Location = new Point(3, 83);
			checkBox_Tag3.MinimumSize = new Size(1, 1);
			checkBox_Tag3.Name = "checkBox_Tag3";
			checkBox_Tag3.Size = new Size(74, 34);
			checkBox_Tag3.TabIndex = 3;
			checkBox_Tag3.Text = "tag3";
			// 
			// checkBox_Tag2
			// 
			checkBox_Tag2.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
			checkBox_Tag2.ForeColor = Color.FromArgb(48, 48, 48);
			checkBox_Tag2.Location = new Point(3, 43);
			checkBox_Tag2.MinimumSize = new Size(1, 1);
			checkBox_Tag2.Name = "checkBox_Tag2";
			checkBox_Tag2.Size = new Size(74, 34);
			checkBox_Tag2.TabIndex = 2;
			checkBox_Tag2.Text = "tag2";
			// 
			// checkBox_Tag1
			// 
			checkBox_Tag1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
			checkBox_Tag1.ForeColor = Color.FromArgb(48, 48, 48);
			checkBox_Tag1.Location = new Point(3, 3);
			checkBox_Tag1.MinimumSize = new Size(1, 1);
			checkBox_Tag1.Name = "checkBox_Tag1";
			checkBox_Tag1.Size = new Size(74, 34);
			checkBox_Tag1.TabIndex = 1;
			checkBox_Tag1.Text = "tag1";
			// 
			// lineChart
			// 
			lineChart.FillColor = Color.White;
			lineChart.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
			lineChart.ForeColor = Color.Black;
			lineChart.LegendFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point);
			lineChart.Location = new Point(17, 20);
			lineChart.MinimumSize = new Size(1, 1);
			lineChart.MouseDownType = Sunny.UI.UILineChartMouseDownType.Zoom;
			lineChart.Name = "lineChart";
			lineChart.RectColor = Color.FromArgb(255, 255, 192);
			lineChart.Size = new Size(709, 471);
			lineChart.SubFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point);
			lineChart.TabIndex = 1;
			lineChart.Text = "uiLineChart1";
			// 
			// btn_Start
			// 
			btn_Start.FillColor = Color.Beige;
			btn_Start.FillColor2 = Color.LightCyan;
			btn_Start.FillColorGradient = true;
			btn_Start.FillColorGradientDirection = FlowDirection.RightToLeft;
			btn_Start.FillHoverColor = Color.LightCyan;
			btn_Start.FillPressColor = Color.Beige;
			btn_Start.FillSelectedColor = Color.Beige;
			btn_Start.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
			btn_Start.ForeColor = Color.Black;
			btn_Start.ForeHoverColor = Color.Black;
			btn_Start.ForePressColor = Color.Black;
			btn_Start.Location = new Point(497, 527);
			btn_Start.MinimumSize = new Size(1, 1);
			btn_Start.Name = "btn_Start";
			btn_Start.Radius = 15;
			btn_Start.RectColor = Color.LightCyan;
			btn_Start.RectHoverColor = Color.Beige;
			btn_Start.RectPressColor = Color.Transparent;
			btn_Start.RectSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Right;
			btn_Start.RectSize = 2;
			btn_Start.Size = new Size(125, 44);
			btn_Start.TabIndex = 13;
			btn_Start.Text = "开始";
			btn_Start.Click += btn_Start_Click;
			// 
			// btn_Stop
			// 
			btn_Stop.FillColor = Color.Beige;
			btn_Stop.FillColor2 = Color.LightCyan;
			btn_Stop.FillColorGradient = true;
			btn_Stop.FillColorGradientDirection = FlowDirection.RightToLeft;
			btn_Stop.FillHoverColor = Color.LightCyan;
			btn_Stop.FillPressColor = Color.Beige;
			btn_Stop.FillSelectedColor = Color.Beige;
			btn_Stop.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
			btn_Stop.ForeColor = Color.Black;
			btn_Stop.ForeHoverColor = Color.Black;
			btn_Stop.ForePressColor = Color.Black;
			btn_Stop.Location = new Point(674, 527);
			btn_Stop.MinimumSize = new Size(1, 1);
			btn_Stop.Name = "btn_Stop";
			btn_Stop.Radius = 15;
			btn_Stop.RectColor = Color.LightCyan;
			btn_Stop.RectHoverColor = Color.Beige;
			btn_Stop.RectPressColor = Color.Transparent;
			btn_Stop.RectSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Right;
			btn_Stop.RectSize = 2;
			btn_Stop.Size = new Size(125, 44);
			btn_Stop.TabIndex = 13;
			btn_Stop.Text = "停止";
			btn_Stop.Click += btn_Stop_Click;
			// 
			// EChart
			// 
			AutoScaleDimensions = new SizeF(9F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(btn_Stop);
			Controls.Add(btn_Start);
			Controls.Add(lineChart);
			Controls.Add(tableLayoutPanel_Tags);
			Name = "EChart";
			Size = new Size(880, 592);
			tableLayoutPanel_Tags.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private Sunny.UI.UITableLayoutPanel tableLayoutPanel_Tags;
		private Sunny.UI.UICheckBox checkBox_Tag1;
		private Sunny.UI.UICheckBox checkBox_Tag5;
		private Sunny.UI.UICheckBox checkBox_Tag4;
		private Sunny.UI.UICheckBox checkBox_Tag3;
		private Sunny.UI.UICheckBox checkBox_Tag2;
		private Sunny.UI.UILineChart lineChart;
		private Sunny.UI.UIButton btn_Start;
		private Sunny.UI.UIButton btn_Stop;
	}
}
