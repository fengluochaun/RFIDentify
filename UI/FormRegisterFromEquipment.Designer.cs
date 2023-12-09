namespace RFIDentify.UI
{
    partial class FormRegisterFromEquipment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRegisterFromEquipment));
            lineChart = new Sunny.UI.UILineChart();
            lbl_Id = new Sunny.UI.UILabel();
            btn_Start = new Sunny.UI.UIButton();
            btn_Stop = new Sunny.UI.UIButton();
            btn_CollectEnvir = new Sunny.UI.UIButton();
            btn_Save = new Sunny.UI.UIButton();
            SuspendLayout();
            // 
            // lineChart
            // 
            lineChart.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lineChart.LegendFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lineChart.Location = new Point(51, 142);
            lineChart.MinimumSize = new Size(1, 1);
            lineChart.MouseDownType = Sunny.UI.UILineChartMouseDownType.Zoom;
            lineChart.Name = "lineChart";
            lineChart.Size = new Size(928, 537);
            lineChart.SubFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lineChart.TabIndex = 4;
            lineChart.Text = "uiLineChart1";
            // 
            // lbl_Id
            // 
            lbl_Id.Font = new Font("宋体", 14F, FontStyle.Regular, GraphicsUnit.Point);
            lbl_Id.ForeColor = Color.FromArgb(48, 48, 48);
            lbl_Id.Location = new Point(51, 52);
            lbl_Id.Name = "lbl_Id";
            lbl_Id.Size = new Size(125, 36);
            lbl_Id.Style = Sunny.UI.UIStyle.Custom;
            lbl_Id.TabIndex = 5;
            lbl_Id.Text = "编号：";
            lbl_Id.TextAlign = ContentAlignment.MiddleCenter;
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
            btn_Start.Location = new Point(331, 52);
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
            btn_Start.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point);
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
            btn_Stop.Location = new Point(505, 52);
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
            btn_Stop.Text = "结束";
            btn_Stop.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btn_Stop.Click += btn_Stop_Click;
            // 
            // btn_CollectEnvir
            // 
            btn_CollectEnvir.FillColor = Color.Beige;
            btn_CollectEnvir.FillColor2 = Color.LightCyan;
            btn_CollectEnvir.FillColorGradient = true;
            btn_CollectEnvir.FillColorGradientDirection = FlowDirection.RightToLeft;
            btn_CollectEnvir.FillHoverColor = Color.LightCyan;
            btn_CollectEnvir.FillPressColor = Color.Beige;
            btn_CollectEnvir.FillSelectedColor = Color.Beige;
            btn_CollectEnvir.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btn_CollectEnvir.ForeColor = Color.Black;
            btn_CollectEnvir.ForeHoverColor = Color.Black;
            btn_CollectEnvir.ForePressColor = Color.Black;
            btn_CollectEnvir.Location = new Point(683, 52);
            btn_CollectEnvir.MinimumSize = new Size(1, 1);
            btn_CollectEnvir.Name = "btn_CollectEnvir";
            btn_CollectEnvir.Radius = 15;
            btn_CollectEnvir.RectColor = Color.LightCyan;
            btn_CollectEnvir.RectHoverColor = Color.Beige;
            btn_CollectEnvir.RectPressColor = Color.Transparent;
            btn_CollectEnvir.RectSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Right;
            btn_CollectEnvir.RectSize = 2;
            btn_CollectEnvir.Size = new Size(125, 44);
            btn_CollectEnvir.TabIndex = 13;
            btn_CollectEnvir.Text = "获取环境相位";
            btn_CollectEnvir.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btn_CollectEnvir.Click += btn_CollectEnvir_Click;
            // 
            // btn_Save
            // 
            btn_Save.FillColor = Color.Beige;
            btn_Save.FillColor2 = Color.LightCyan;
            btn_Save.FillColorGradient = true;
            btn_Save.FillColorGradientDirection = FlowDirection.RightToLeft;
            btn_Save.FillHoverColor = Color.LightCyan;
            btn_Save.FillPressColor = Color.Beige;
            btn_Save.FillSelectedColor = Color.Beige;
            btn_Save.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btn_Save.ForeColor = Color.Black;
            btn_Save.ForeHoverColor = Color.Black;
            btn_Save.ForePressColor = Color.Black;
            btn_Save.Location = new Point(854, 52);
            btn_Save.MinimumSize = new Size(1, 1);
            btn_Save.Name = "btn_Save";
            btn_Save.Radius = 15;
            btn_Save.RectColor = Color.LightCyan;
            btn_Save.RectHoverColor = Color.Beige;
            btn_Save.RectPressColor = Color.Transparent;
            btn_Save.RectSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Right;
            btn_Save.RectSize = 2;
            btn_Save.Size = new Size(125, 44);
            btn_Save.TabIndex = 13;
            btn_Save.Text = "保存";
            btn_Save.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btn_Save.Click += btn_Save_Click;
            // 
            // FormRegisterFromEquipment
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1030, 700);
            Controls.Add(btn_CollectEnvir);
            Controls.Add(btn_Save);
            Controls.Add(btn_Stop);
            Controls.Add(btn_Start);
            Controls.Add(lbl_Id);
            Controls.Add(lineChart);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormRegisterFromEquipment";
            Text = "FormRegisterFromByEquipment";
            ZoomScaleRect = new Rectangle(19, 19, 800, 450);
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UILineChart lineChart;
        private Sunny.UI.UILabel lbl_Id;
        private Sunny.UI.UIButton btn_Start;
        private Sunny.UI.UIButton btn_Stop;
        private Sunny.UI.UIButton btn_CollectEnvir;
        private Sunny.UI.UIButton btn_Save;
    }
}