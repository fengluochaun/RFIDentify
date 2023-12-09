namespace RFIDentify.UI
{
    partial class FormRegister
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
            uiLabel1 = new Sunny.UI.UILabel();
            txt_Id = new Sunny.UI.UITextBox();
            txt_Age = new Sunny.UI.UITextBox();
            uiLabel2 = new Sunny.UI.UILabel();
            txt_Telephone = new Sunny.UI.UITextBox();
            uiLabel3 = new Sunny.UI.UILabel();
            txt_Name = new Sunny.UI.UITextBox();
            uiLabel4 = new Sunny.UI.UILabel();
            txt_Description = new Sunny.UI.UITextBox();
            uiLabel5 = new Sunny.UI.UILabel();
            uiLabel6 = new Sunny.UI.UILabel();
            listBox = new Sunny.UI.UIListBox();
            roundProcess = new Sunny.UI.UIRoundProcess();
            btn_Save = new Sunny.UI.UIButton();
            btn_FromEquipment = new Sunny.UI.UIButton();
            btn_FromExplorer = new Sunny.UI.UIButton();
            btn_Commit = new Sunny.UI.UIButton();
            btn_UploadPhoto = new Sunny.UI.UIButton();
            SuspendLayout();
            // 
            // uiLabel1
            // 
            uiLabel1.Font = new Font("宋体", 14F, FontStyle.Regular, GraphicsUnit.Point);
            uiLabel1.ForeColor = Color.FromArgb(48, 48, 48);
            uiLabel1.Location = new Point(55, 69);
            uiLabel1.Name = "uiLabel1";
            uiLabel1.Size = new Size(125, 36);
            uiLabel1.Style = Sunny.UI.UIStyle.Custom;
            uiLabel1.TabIndex = 0;
            uiLabel1.Text = "编号：";
            uiLabel1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txt_Id
            // 
            txt_Id.ButtonFillColor = Color.White;
            txt_Id.ButtonFillHoverColor = Color.White;
            txt_Id.ButtonFillPressColor = Color.White;
            txt_Id.ButtonRectColor = Color.White;
            txt_Id.ButtonRectHoverColor = Color.White;
            txt_Id.ButtonRectPressColor = Color.White;
            txt_Id.ButtonStyleInherited = false;
            txt_Id.DoubleValue = 1D;
            txt_Id.FillReadOnlyColor = Color.White;
            txt_Id.Font = new Font("宋体", 14F, FontStyle.Regular, GraphicsUnit.Point);
            txt_Id.ForeReadOnlyColor = Color.Black;
            txt_Id.IntValue = 1;
            txt_Id.Location = new Point(187, 69);
            txt_Id.Margin = new Padding(4, 5, 4, 5);
            txt_Id.MinimumSize = new Size(1, 16);
            txt_Id.Name = "txt_Id";
            txt_Id.Padding = new Padding(5);
            txt_Id.ReadOnly = true;
            txt_Id.RectColor = Color.Peru;
            txt_Id.RectReadOnlyColor = Color.FromArgb(255, 224, 192);
            txt_Id.ShowText = false;
            txt_Id.Size = new Size(86, 36);
            txt_Id.TabIndex = 1;
            txt_Id.Text = "1";
            txt_Id.TextAlignment = ContentAlignment.MiddleLeft;
            txt_Id.Watermark = "";
            // 
            // txt_Age
            // 
            txt_Age.ButtonFillColor = Color.White;
            txt_Age.ButtonFillHoverColor = Color.White;
            txt_Age.ButtonFillPressColor = Color.White;
            txt_Age.ButtonRectColor = Color.White;
            txt_Age.ButtonRectHoverColor = Color.White;
            txt_Age.ButtonRectPressColor = Color.White;
            txt_Age.ButtonStyleInherited = false;
            txt_Age.DoubleValue = 18D;
            txt_Age.Font = new Font("宋体", 14F, FontStyle.Regular, GraphicsUnit.Point);
            txt_Age.IntValue = 18;
            txt_Age.Location = new Point(187, 143);
            txt_Age.Margin = new Padding(4, 5, 4, 5);
            txt_Age.MinimumSize = new Size(1, 16);
            txt_Age.Name = "txt_Age";
            txt_Age.Padding = new Padding(5);
            txt_Age.RectColor = Color.PeachPuff;
            txt_Age.ShowText = false;
            txt_Age.Size = new Size(86, 36);
            txt_Age.TabIndex = 3;
            txt_Age.Text = "18";
            txt_Age.TextAlignment = ContentAlignment.MiddleLeft;
            txt_Age.Watermark = "";
            // 
            // uiLabel2
            // 
            uiLabel2.Font = new Font("宋体", 14F, FontStyle.Regular, GraphicsUnit.Point);
            uiLabel2.ForeColor = Color.FromArgb(48, 48, 48);
            uiLabel2.Location = new Point(55, 143);
            uiLabel2.Name = "uiLabel2";
            uiLabel2.Size = new Size(125, 36);
            uiLabel2.Style = Sunny.UI.UIStyle.Custom;
            uiLabel2.TabIndex = 2;
            uiLabel2.Text = "年龄：";
            uiLabel2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txt_Telephone
            // 
            txt_Telephone.ButtonFillColor = Color.White;
            txt_Telephone.ButtonFillHoverColor = Color.White;
            txt_Telephone.ButtonFillPressColor = Color.White;
            txt_Telephone.ButtonRectColor = Color.White;
            txt_Telephone.ButtonRectHoverColor = Color.White;
            txt_Telephone.ButtonRectPressColor = Color.White;
            txt_Telephone.ButtonStyleInherited = false;
            txt_Telephone.Font = new Font("宋体", 14F, FontStyle.Regular, GraphicsUnit.Point);
            txt_Telephone.Location = new Point(472, 143);
            txt_Telephone.Margin = new Padding(4, 5, 4, 5);
            txt_Telephone.MinimumSize = new Size(1, 16);
            txt_Telephone.Name = "txt_Telephone";
            txt_Telephone.Padding = new Padding(5);
            txt_Telephone.RectColor = Color.PeachPuff;
            txt_Telephone.ShowText = false;
            txt_Telephone.Size = new Size(184, 36);
            txt_Telephone.TabIndex = 7;
            txt_Telephone.Text = "172-1529-2832";
            txt_Telephone.TextAlignment = ContentAlignment.MiddleLeft;
            txt_Telephone.Watermark = "";
            // 
            // uiLabel3
            // 
            uiLabel3.Font = new Font("宋体", 14F, FontStyle.Regular, GraphicsUnit.Point);
            uiLabel3.ForeColor = Color.FromArgb(48, 48, 48);
            uiLabel3.Location = new Point(316, 143);
            uiLabel3.Name = "uiLabel3";
            uiLabel3.Size = new Size(149, 36);
            uiLabel3.Style = Sunny.UI.UIStyle.Custom;
            uiLabel3.TabIndex = 6;
            uiLabel3.Text = "电话号码：";
            uiLabel3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txt_Name
            // 
            txt_Name.ButtonFillColor = Color.White;
            txt_Name.ButtonFillHoverColor = Color.White;
            txt_Name.ButtonFillPressColor = Color.White;
            txt_Name.ButtonRectColor = Color.White;
            txt_Name.ButtonRectHoverColor = Color.White;
            txt_Name.ButtonRectPressColor = Color.White;
            txt_Name.ButtonStyleInherited = false;
            txt_Name.Font = new Font("宋体", 14F, FontStyle.Regular, GraphicsUnit.Point);
            txt_Name.Location = new Point(472, 69);
            txt_Name.Margin = new Padding(4, 5, 4, 5);
            txt_Name.MinimumSize = new Size(1, 16);
            txt_Name.Name = "txt_Name";
            txt_Name.Padding = new Padding(5);
            txt_Name.RectColor = Color.PeachPuff;
            txt_Name.ShowText = false;
            txt_Name.Size = new Size(184, 36);
            txt_Name.TabIndex = 5;
            txt_Name.Text = "Lambert";
            txt_Name.TextAlignment = ContentAlignment.MiddleLeft;
            txt_Name.Watermark = "";
            // 
            // uiLabel4
            // 
            uiLabel4.Font = new Font("宋体", 14F, FontStyle.Regular, GraphicsUnit.Point);
            uiLabel4.ForeColor = Color.FromArgb(48, 48, 48);
            uiLabel4.Location = new Point(316, 69);
            uiLabel4.Name = "uiLabel4";
            uiLabel4.Size = new Size(125, 36);
            uiLabel4.Style = Sunny.UI.UIStyle.Custom;
            uiLabel4.TabIndex = 4;
            uiLabel4.Text = "姓名：";
            uiLabel4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // txt_Description
            // 
            txt_Description.ButtonFillColor = Color.Gray;
            txt_Description.ButtonFillHoverColor = Color.White;
            txt_Description.ButtonFillPressColor = Color.White;
            txt_Description.ButtonRectColor = Color.White;
            txt_Description.ButtonRectHoverColor = Color.White;
            txt_Description.ButtonRectPressColor = Color.White;
            txt_Description.ButtonStyleInherited = false;
            txt_Description.Font = new Font("宋体", 14F, FontStyle.Regular, GraphicsUnit.Point);
            txt_Description.Location = new Point(187, 220);
            txt_Description.Margin = new Padding(4, 5, 4, 5);
            txt_Description.MinimumSize = new Size(1, 16);
            txt_Description.Multiline = true;
            txt_Description.Name = "txt_Description";
            txt_Description.Padding = new Padding(5);
            txt_Description.RectColor = Color.PeachPuff;
            txt_Description.ShowText = false;
            txt_Description.Size = new Size(469, 107);
            txt_Description.TabIndex = 9;
            txt_Description.Text = "！！！faef";
            txt_Description.TextAlignment = ContentAlignment.TopLeft;
            txt_Description.Watermark = "";
            // 
            // uiLabel5
            // 
            uiLabel5.Font = new Font("宋体", 14F, FontStyle.Regular, GraphicsUnit.Point);
            uiLabel5.ForeColor = Color.FromArgb(48, 48, 48);
            uiLabel5.Location = new Point(55, 220);
            uiLabel5.Name = "uiLabel5";
            uiLabel5.Size = new Size(125, 36);
            uiLabel5.Style = Sunny.UI.UIStyle.Custom;
            uiLabel5.TabIndex = 8;
            uiLabel5.Text = "描述：";
            uiLabel5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // uiLabel6
            // 
            uiLabel6.Font = new Font("宋体", 14F, FontStyle.Regular, GraphicsUnit.Point);
            uiLabel6.ForeColor = Color.FromArgb(48, 48, 48);
            uiLabel6.Location = new Point(55, 359);
            uiLabel6.Name = "uiLabel6";
            uiLabel6.Size = new Size(136, 36);
            uiLabel6.Style = Sunny.UI.UIStyle.Custom;
            uiLabel6.TabIndex = 8;
            uiLabel6.Text = "注册文件：";
            uiLabel6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // listBox
            // 
            listBox.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
            listBox.HoverColor = Color.FromArgb(255, 224, 192);
            listBox.ItemSelectBackColor = Color.FromArgb(255, 192, 128);
            listBox.ItemSelectForeColor = Color.White;
            listBox.Location = new Point(198, 359);
            listBox.Margin = new Padding(4, 5, 4, 5);
            listBox.MinimumSize = new Size(1, 1);
            listBox.Name = "listBox";
            listBox.Padding = new Padding(2);
            listBox.RectColor = Color.LightSalmon;
            listBox.ScrollBarColor = Color.Silver;
            listBox.ScrollBarStyleInherited = false;
            listBox.ShowText = false;
            listBox.Size = new Size(458, 311);
            listBox.TabIndex = 10;
            listBox.Text = "uiListBox1";
            // 
            // roundProcess
            // 
            roundProcess.DecimalPlaces = 0;
            roundProcess.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
            roundProcess.ForeColor2 = Color.Black;
            roundProcess.Inner = 45;
            roundProcess.Location = new Point(753, 32);
            roundProcess.MinimumSize = new Size(1, 1);
            roundProcess.Name = "roundProcess";
            roundProcess.Outer = 80;
            roundProcess.ShowProcess = true;
            roundProcess.Size = new Size(183, 187);
            roundProcess.StartAngle = 220;
            roundProcess.SweepAngle = 280;
            roundProcess.TabIndex = 11;
            roundProcess.Text = "50%";
            roundProcess.Value = 50;
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
            btn_Save.Location = new Point(780, 270);
            btn_Save.MinimumSize = new Size(1, 1);
            btn_Save.Name = "btn_Save";
            btn_Save.Radius = 15;
            btn_Save.RectColor = Color.LightCyan;
            btn_Save.RectHoverColor = Color.Beige;
            btn_Save.RectPressColor = Color.Transparent;
            btn_Save.RectSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Right;
            btn_Save.RectSize = 2;
            btn_Save.Size = new Size(125, 44);
            btn_Save.TabIndex = 12;
            btn_Save.Text = "保存";
            btn_Save.Click += btn_Save_Click;
            // 
            // btn_FromEquipment
            // 
            btn_FromEquipment.FillColor = Color.Beige;
            btn_FromEquipment.FillColor2 = Color.LightCyan;
            btn_FromEquipment.FillColorGradient = true;
            btn_FromEquipment.FillColorGradientDirection = FlowDirection.RightToLeft;
            btn_FromEquipment.FillHoverColor = Color.LightCyan;
            btn_FromEquipment.FillPressColor = Color.Beige;
            btn_FromEquipment.FillSelectedColor = Color.Beige;
            btn_FromEquipment.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btn_FromEquipment.ForeColor = Color.Black;
            btn_FromEquipment.ForeHoverColor = Color.Black;
            btn_FromEquipment.ForePressColor = Color.Black;
            btn_FromEquipment.Location = new Point(780, 434);
            btn_FromEquipment.MinimumSize = new Size(1, 1);
            btn_FromEquipment.Name = "btn_FromEquipment";
            btn_FromEquipment.Radius = 15;
            btn_FromEquipment.RectColor = Color.LightCyan;
            btn_FromEquipment.RectHoverColor = Color.Beige;
            btn_FromEquipment.RectPressColor = Color.Transparent;
            btn_FromEquipment.RectSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Right;
            btn_FromEquipment.RectSize = 2;
            btn_FromEquipment.Size = new Size(125, 44);
            btn_FromEquipment.TabIndex = 12;
            btn_FromEquipment.Text = "从设备";
            btn_FromEquipment.Click += btn_FromEquipment_Click;
            // 
            // btn_FromExplorer
            // 
            btn_FromExplorer.FillColor = Color.Beige;
            btn_FromExplorer.FillColor2 = Color.LightCyan;
            btn_FromExplorer.FillColorGradient = true;
            btn_FromExplorer.FillColorGradientDirection = FlowDirection.RightToLeft;
            btn_FromExplorer.FillHoverColor = Color.LightCyan;
            btn_FromExplorer.FillPressColor = Color.Beige;
            btn_FromExplorer.FillSelectedColor = Color.Beige;
            btn_FromExplorer.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btn_FromExplorer.ForeColor = Color.Black;
            btn_FromExplorer.ForeHoverColor = Color.Black;
            btn_FromExplorer.ForePressColor = Color.Black;
            btn_FromExplorer.Location = new Point(780, 521);
            btn_FromExplorer.MinimumSize = new Size(1, 1);
            btn_FromExplorer.Name = "btn_FromExplorer";
            btn_FromExplorer.Radius = 15;
            btn_FromExplorer.RectColor = Color.LightCyan;
            btn_FromExplorer.RectHoverColor = Color.Beige;
            btn_FromExplorer.RectPressColor = Color.Transparent;
            btn_FromExplorer.RectSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Right;
            btn_FromExplorer.RectSize = 2;
            btn_FromExplorer.Size = new Size(125, 44);
            btn_FromExplorer.TabIndex = 12;
            btn_FromExplorer.Text = "从文件";
            btn_FromExplorer.Click += btn_FromExplorer_Click;
            // 
            // btn_Commit
            // 
            btn_Commit.FillColor = Color.Beige;
            btn_Commit.FillColor2 = Color.LightCyan;
            btn_Commit.FillColorGradient = true;
            btn_Commit.FillColorGradientDirection = FlowDirection.RightToLeft;
            btn_Commit.FillHoverColor = Color.LightCyan;
            btn_Commit.FillPressColor = Color.Beige;
            btn_Commit.FillSelectedColor = Color.Beige;
            btn_Commit.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btn_Commit.ForeColor = Color.Black;
            btn_Commit.ForeHoverColor = Color.Black;
            btn_Commit.ForePressColor = Color.Black;
            btn_Commit.Location = new Point(780, 600);
            btn_Commit.MinimumSize = new Size(1, 1);
            btn_Commit.Name = "btn_Commit";
            btn_Commit.Radius = 15;
            btn_Commit.RectColor = Color.LightCyan;
            btn_Commit.RectHoverColor = Color.Beige;
            btn_Commit.RectPressColor = Color.Transparent;
            btn_Commit.RectSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Right;
            btn_Commit.RectSize = 2;
            btn_Commit.Size = new Size(125, 44);
            btn_Commit.TabIndex = 12;
            btn_Commit.Text = "提交";
            btn_Commit.Click += btn_Commit_Click;
            // 
            // btn_UploadPhoto
            // 
            btn_UploadPhoto.FillColor = Color.Beige;
            btn_UploadPhoto.FillColor2 = Color.LightCyan;
            btn_UploadPhoto.FillColorGradient = true;
            btn_UploadPhoto.FillColorGradientDirection = FlowDirection.RightToLeft;
            btn_UploadPhoto.FillHoverColor = Color.LightCyan;
            btn_UploadPhoto.FillPressColor = Color.Beige;
            btn_UploadPhoto.FillSelectedColor = Color.Beige;
            btn_UploadPhoto.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
            btn_UploadPhoto.ForeColor = Color.Black;
            btn_UploadPhoto.ForeHoverColor = Color.Black;
            btn_UploadPhoto.ForePressColor = Color.Black;
            btn_UploadPhoto.Location = new Point(780, 351);
            btn_UploadPhoto.MinimumSize = new Size(1, 1);
            btn_UploadPhoto.Name = "btn_UploadPhoto";
            btn_UploadPhoto.Radius = 15;
            btn_UploadPhoto.RectColor = Color.LightCyan;
            btn_UploadPhoto.RectHoverColor = Color.Beige;
            btn_UploadPhoto.RectPressColor = Color.Transparent;
            btn_UploadPhoto.RectSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Right;
            btn_UploadPhoto.RectSize = 2;
            btn_UploadPhoto.Size = new Size(125, 44);
            btn_UploadPhoto.TabIndex = 12;
            btn_UploadPhoto.Text = "上传头像";
            btn_UploadPhoto.TipsFont = new Font("宋体", 9F, FontStyle.Regular, GraphicsUnit.Point);
            btn_UploadPhoto.Click += btn_UploadPhoto_Click;
            // 
            // FormRegister
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1030, 700);
            Controls.Add(btn_Commit);
            Controls.Add(btn_FromExplorer);
            Controls.Add(btn_FromEquipment);
            Controls.Add(btn_UploadPhoto);
            Controls.Add(btn_Save);
            Controls.Add(roundProcess);
            Controls.Add(listBox);
            Controls.Add(txt_Description);
            Controls.Add(uiLabel6);
            Controls.Add(uiLabel5);
            Controls.Add(txt_Telephone);
            Controls.Add(uiLabel3);
            Controls.Add(txt_Name);
            Controls.Add(uiLabel4);
            Controls.Add(txt_Age);
            Controls.Add(uiLabel2);
            Controls.Add(txt_Id);
            Controls.Add(uiLabel1);
            Name = "FormRegister";
            Text = "FormRegister";
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UITextBox txt_Id;
        private Sunny.UI.UITextBox txt_Age;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UITextBox txt_Telephone;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UITextBox txt_Name;
        private Sunny.UI.UILabel uiLabel4;
        private Sunny.UI.UITextBox txt_Description;
        private Sunny.UI.UILabel uiLabel5;
        private Sunny.UI.UILabel uiLabel6;
        private Sunny.UI.UIListBox listBox;
        private Sunny.UI.UIRoundProcess roundProcess;
        private Sunny.UI.UIButton btn_Save;
        private Sunny.UI.UIButton btn_FromEquipment;
        private Sunny.UI.UIButton btn_FromExplorer;
        private Sunny.UI.UIButton btn_Commit;
        private Sunny.UI.UIButton btn_UploadPhoto;
    }
}