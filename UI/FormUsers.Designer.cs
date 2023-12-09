namespace RFIDentify.UI
{
    partial class FormUsers
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
            col_Id = new DataGridViewTextBoxColumn();
            Col_Name = new DataGridViewTextBoxColumn();
            col_Age = new DataGridViewTextBoxColumn();
            col_Telephone = new DataGridViewTextBoxColumn();
            col_Description = new DataGridViewTextBoxColumn();
            lbl1 = new Sunny.UI.UILabel();
            userBindingSource = new BindingSource(components);
            DGV_Users = new Sunny.UI.UIDataGridView();
            ((System.ComponentModel.ISupportInitialize)userBindingSource).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DGV_Users).BeginInit();
            SuspendLayout();
            // 
            // col_Id
            // 
            col_Id.DataPropertyName = "Id";
            col_Id.FillWeight = 75F;
            col_Id.HeaderText = "编号";
            col_Id.MinimumWidth = 6;
            col_Id.Name = "col_Id";
            col_Id.ReadOnly = true;
            col_Id.Width = 125;
            // 
            // Col_Name
            // 
            Col_Name.DataPropertyName = "Name";
            Col_Name.HeaderText = "姓名";
            Col_Name.MinimumWidth = 6;
            Col_Name.Name = "Col_Name";
            Col_Name.Width = 125;
            // 
            // col_Age
            // 
            col_Age.DataPropertyName = "Age";
            col_Age.FillWeight = 75F;
            col_Age.HeaderText = "年龄";
            col_Age.MinimumWidth = 6;
            col_Age.Name = "col_Age";
            col_Age.Width = 125;
            // 
            // col_Telephone
            // 
            col_Telephone.DataPropertyName = "Telephone";
            col_Telephone.FillWeight = 125F;
            col_Telephone.HeaderText = "电话号码";
            col_Telephone.MinimumWidth = 6;
            col_Telephone.Name = "col_Telephone";
            col_Telephone.Width = 125;
            // 
            // col_Description
            // 
            col_Description.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            col_Description.DataPropertyName = "Description";
            col_Description.HeaderText = "描述";
            col_Description.MinimumWidth = 6;
            col_Description.Name = "col_Description";
            // 
            // lbl1
            // 
            lbl1.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lbl1.ForeColor = Color.FromArgb(48, 48, 48);
            lbl1.Location = new Point(47, 64);
            lbl1.Name = "lbl1";
            lbl1.Size = new Size(208, 29);
            lbl1.TabIndex = 2;
            lbl1.Text = "已认证人员信息列表";
            lbl1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // userBindingSource
            // 
            userBindingSource.DataSource = typeof(Models.User);
            // 
            // DGV_Users
            // 
            dataGridViewCellStyle1.BackColor = Color.FromArgb(235, 243, 255);
            DGV_Users.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            DGV_Users.BackgroundColor = Color.White;
            DGV_Users.BorderStyle = BorderStyle.Fixed3D;
            DGV_Users.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = Color.White;
            dataGridViewCellStyle2.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = Color.AntiqueWhite;
            dataGridViewCellStyle2.SelectionForeColor = Color.Black;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            DGV_Users.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            DGV_Users.ColumnHeadersHeight = 50;
            DGV_Users.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            DGV_Users.Columns.AddRange(new DataGridViewColumn[] { col_Id, Col_Name, col_Age, col_Telephone, col_Description });
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle3.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            DGV_Users.DefaultCellStyle = dataGridViewCellStyle3;
            DGV_Users.EnableHeadersVisualStyles = false;
            DGV_Users.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
            DGV_Users.GridColor = Color.White;
            DGV_Users.Location = new Point(47, 141);
            DGV_Users.Name = "DGV_Users";
            DGV_Users.RectColor = Color.White;
            DGV_Users.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.Snow;
            dataGridViewCellStyle4.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle4.ForeColor = Color.FromArgb(48, 48, 48);
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(128, 255, 255);
            dataGridViewCellStyle4.SelectionForeColor = Color.Black;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            DGV_Users.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            DGV_Users.RowHeadersWidth = 51;
            dataGridViewCellStyle5.BackColor = Color.White;
            dataGridViewCellStyle5.Font = new Font("宋体", 12F, FontStyle.Regular, GraphicsUnit.Point);
            dataGridViewCellStyle5.ForeColor = Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = Color.OldLace;
            dataGridViewCellStyle5.SelectionForeColor = Color.Black;
            DGV_Users.RowsDefaultCellStyle = dataGridViewCellStyle5;
            DGV_Users.RowTemplate.Height = 40;
            DGV_Users.ScrollBarColor = Color.Silver;
            DGV_Users.ScrollBarRectColor = Color.White;
            DGV_Users.ScrollBarStyleInherited = false;
            DGV_Users.SelectedIndex = -1;
            DGV_Users.Size = new Size(936, 528);
            DGV_Users.StripeOddColor = Color.FromArgb(235, 243, 255);
            DGV_Users.TabIndex = 1;
            DGV_Users.CellContentDoubleClick += DGV_Users_CellContentDoubleClick;
            DGV_Users.CellMouseClick += DGV_Users_CellMouseClick;
            // 
            // FormUsers
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1030, 700);
            Controls.Add(lbl1);
            Controls.Add(DGV_Users);
            Name = "FormUsers";
            Text = "FormUsers";
            Load += FormUsers_Load;
            ((System.ComponentModel.ISupportInitialize)userBindingSource).EndInit();
            ((System.ComponentModel.ISupportInitialize)DGV_Users).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Sunny.UI.UIDataGridView DGV_Users;
        private Sunny.UI.UILabel lbl1;
        private BindingSource userBindingSource;
        private DataGridViewTextBoxColumn col_Id;
        private DataGridViewTextBoxColumn Col_Name;
        private DataGridViewTextBoxColumn col_Age;
        private DataGridViewTextBoxColumn col_Telephone;
        private DataGridViewTextBoxColumn col_Description;
    }
}