namespace RFIDentify.UI
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            SuspendLayout();
            // 
            // Aside
            // 
            Aside.BackColor = Color.Wheat;
            Aside.FillColor = Color.Wheat;
            Aside.Font = new Font("黑体", 13F, FontStyle.Regular, GraphicsUnit.Point);
            Aside.ForeColor = Color.Black;
            Aside.FullRowSelect = false;
            Aside.HideSelection = false;
            Aside.HoverColor = Color.PapayaWhip;
            Aside.LineColor = Color.Black;
            Aside.Location = new Point(0, 50);
            Aside.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            Aside.SecondBackColor = Color.FromArgb(255, 224, 192);
            Aside.SelectedColor = Color.FromArgb(255, 224, 192);
            Aside.SelectedColor2 = Color.FromArgb(255, 192, 128);
            Aside.SelectedForeColor = Color.Black;
            Aside.SelectedHighColor = Color.FromArgb(255, 192, 128);
            Aside.Size = new Size(180, 700);
            // 
            // FormMain
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1210, 750);
            HelpButton = true;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "FormMain";
            Padding = new Padding(0, 50, 0, 0);
            RectColor = Color.FromArgb(224, 224, 224);
            Text = " RFIDentify";
            TitleColor = Color.PapayaWhip;
            TitleFont = new Font("MV Boli", 17F, FontStyle.Bold, GraphicsUnit.Point);
            TitleForeColor = Color.DimGray;
            TitleHeight = 50;
            TransparencyKey = Color.Black;
            ZoomScaleRect = new Rectangle(19, 19, 922, 525);
            ResumeLayout(false);
        }

        #endregion
    }
}