namespace RFIDentify.UI
{
    partial class FormIdentify
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
            lbl_Identification = new Label();
            btn_Start = new Button();
            btn_Stop = new Button();
            btn_AddUser = new Button();
            plot = new ScottPlot.FormsPlot();
            lineChart = new Sunny.UI.UILineChart();
            SuspendLayout();
            // 
            // lbl_Identification
            // 
            lbl_Identification.AutoSize = true;
            lbl_Identification.Font = new Font("Microsoft YaHei UI", 15F, FontStyle.Regular, GraphicsUnit.Point);
            lbl_Identification.Location = new Point(58, 65);
            lbl_Identification.Name = "lbl_Identification";
            lbl_Identification.Size = new Size(169, 32);
            lbl_Identification.TabIndex = 0;
            lbl_Identification.Text = "识别对象：12";
            lbl_Identification.Click += lbl_Identifcation_Click;
            // 
            // btn_Start
            // 
            btn_Start.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            btn_Start.Location = new Point(538, 67);
            btn_Start.Name = "btn_Start";
            btn_Start.Size = new Size(116, 35);
            btn_Start.TabIndex = 1;
            btn_Start.Text = "开始";
            btn_Start.UseVisualStyleBackColor = true;
            btn_Start.Click += btn_Start_Click;
            // 
            // btn_Stop
            // 
            btn_Stop.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            btn_Stop.Location = new Point(706, 67);
            btn_Stop.Name = "btn_Stop";
            btn_Stop.Size = new Size(116, 35);
            btn_Stop.TabIndex = 1;
            btn_Stop.Text = "停止";
            btn_Stop.UseVisualStyleBackColor = true;
            btn_Stop.Click += btn_Stop_Click;
            // 
            // btn_AddUser
            // 
            btn_AddUser.Font = new Font("Microsoft YaHei UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            btn_AddUser.Location = new Point(870, 67);
            btn_AddUser.Name = "btn_AddUser";
            btn_AddUser.Size = new Size(116, 35);
            btn_AddUser.TabIndex = 1;
            btn_AddUser.Text = "添加用户";
            btn_AddUser.UseVisualStyleBackColor = true;
            btn_AddUser.Click += btn_AddUser_Click;
            // 
            // plot
            // 
            plot.Location = new Point(247, 24);
            plot.Margin = new Padding(5, 4, 5, 4);
            plot.Name = "plot";
            plot.Size = new Size(269, 130);
            plot.TabIndex = 2;
            // 
            // lineChart
            // 
            lineChart.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lineChart.LegendFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lineChart.Location = new Point(58, 151);
            lineChart.MinimumSize = new Size(1, 1);
            lineChart.MouseDownType = Sunny.UI.UILineChartMouseDownType.Zoom;
            lineChart.Name = "lineChart";
            lineChart.Size = new Size(928, 537);
            lineChart.SubFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lineChart.TabIndex = 3;
            lineChart.Text = "uiLineChart1";
            // 
            // FormIdentify
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1030, 700);
            Controls.Add(lineChart);
            Controls.Add(plot);
            Controls.Add(btn_AddUser);
            Controls.Add(btn_Stop);
            Controls.Add(btn_Start);
            Controls.Add(lbl_Identification);
            Name = "FormIdentify";
            Text = "FormIdentify";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbl_Identification;
        private Button btn_Start;
        private Button btn_Stop;
        private Button btn_AddUser;
        private ScottPlot.FormsPlot plot;
        private Sunny.UI.UILineChart lineChart;
    }
}