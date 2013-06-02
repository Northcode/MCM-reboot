namespace MCM_reboot
{
    partial class Form1
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

<<<<<<< HEAD
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
=======
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
>>>>>>> 694be5f933a793b567d4c9b92a34c0da9eff153b
            this.Titlebar = new System.Windows.Forms.Panel();
            this.titleMin = new System.Windows.Forms.Button();
            this.titleMax = new System.Windows.Forms.Button();
            this.titleClose = new System.Windows.Forms.Button();
            this.mainContent = new System.Windows.Forms.Panel();
<<<<<<< HEAD
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.picSlideShow = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
=======
            this.button1 = new System.Windows.Forms.Button();
            this.picSlideShow = new System.Windows.Forms.PictureBox();
>>>>>>> 694be5f933a793b567d4c9b92a34c0da9eff153b
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Titlebar.SuspendLayout();
            this.mainContent.SuspendLayout();
<<<<<<< HEAD
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
=======
>>>>>>> 694be5f933a793b567d4c9b92a34c0da9eff153b
            ((System.ComponentModel.ISupportInitialize)(this.picSlideShow)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Titlebar
            // 
            this.Titlebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(200)))), ((int)(((byte)(245)))));
            this.Titlebar.Controls.Add(this.titleMin);
            this.Titlebar.Controls.Add(this.titleMax);
            this.Titlebar.Controls.Add(this.titleClose);
            this.Titlebar.Dock = System.Windows.Forms.DockStyle.Top;
            this.Titlebar.Location = new System.Drawing.Point(0, 0);
            this.Titlebar.Name = "Titlebar";
<<<<<<< HEAD
            this.Titlebar.Size = new System.Drawing.Size(639, 34);
=======
            this.Titlebar.Size = new System.Drawing.Size(935, 34);
>>>>>>> 694be5f933a793b567d4c9b92a34c0da9eff153b
            this.Titlebar.TabIndex = 0;
            this.Titlebar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.Titlebar.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.Titlebar.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // titleMin
            // 
            this.titleMin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(177)))), ((int)(((byte)(245)))));
            this.titleMin.Dock = System.Windows.Forms.DockStyle.Right;
            this.titleMin.FlatAppearance.BorderSize = 0;
            this.titleMin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
<<<<<<< HEAD
            this.titleMin.Location = new System.Drawing.Point(525, 0);
=======
            this.titleMin.Location = new System.Drawing.Point(821, 0);
>>>>>>> 694be5f933a793b567d4c9b92a34c0da9eff153b
            this.titleMin.Name = "titleMin";
            this.titleMin.Size = new System.Drawing.Size(37, 34);
            this.titleMin.TabIndex = 2;
            this.titleMin.Text = "_";
            this.titleMin.UseVisualStyleBackColor = false;
            this.titleMin.Click += new System.EventHandler(this.button3_Click);
            // 
            // titleMax
            // 
            this.titleMax.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(177)))), ((int)(((byte)(245)))));
            this.titleMax.Dock = System.Windows.Forms.DockStyle.Right;
            this.titleMax.FlatAppearance.BorderSize = 0;
            this.titleMax.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
<<<<<<< HEAD
            this.titleMax.Location = new System.Drawing.Point(562, 0);
=======
            this.titleMax.Location = new System.Drawing.Point(858, 0);
>>>>>>> 694be5f933a793b567d4c9b92a34c0da9eff153b
            this.titleMax.Name = "titleMax";
            this.titleMax.Size = new System.Drawing.Size(33, 34);
            this.titleMax.TabIndex = 1;
            this.titleMax.Text = "^";
            this.titleMax.UseVisualStyleBackColor = false;
            this.titleMax.Click += new System.EventHandler(this.button2_Click);
            // 
            // titleClose
            // 
            this.titleClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(83)))), ((int)(((byte)(177)))), ((int)(((byte)(245)))));
            this.titleClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.titleClose.FlatAppearance.BorderSize = 0;
            this.titleClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
<<<<<<< HEAD
            this.titleClose.Location = new System.Drawing.Point(595, 0);
=======
            this.titleClose.Location = new System.Drawing.Point(891, 0);
>>>>>>> 694be5f933a793b567d4c9b92a34c0da9eff153b
            this.titleClose.Name = "titleClose";
            this.titleClose.Size = new System.Drawing.Size(44, 34);
            this.titleClose.TabIndex = 0;
            this.titleClose.Text = "X";
            this.titleClose.UseVisualStyleBackColor = false;
            this.titleClose.Click += new System.EventHandler(this.button1_Click);
            // 
            // mainContent
            // 
            this.mainContent.AutoScroll = true;
            this.mainContent.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
<<<<<<< HEAD
            this.mainContent.Controls.Add(this.tabControl1);
            this.mainContent.Controls.Add(this.panel5);
=======
            this.mainContent.Controls.Add(this.button1);
            this.mainContent.Controls.Add(this.picSlideShow);
>>>>>>> 694be5f933a793b567d4c9b92a34c0da9eff153b
            this.mainContent.Controls.Add(this.panel1);
            this.mainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContent.Location = new System.Drawing.Point(4, 34);
            this.mainContent.Name = "mainContent";
<<<<<<< HEAD
            this.mainContent.Size = new System.Drawing.Size(635, 433);
            this.mainContent.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(631, 350);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.picSlideShow);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(623, 324);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // picSlideShow
            // 
            this.picSlideShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picSlideShow.Image = global::MCM_reboot.Properties.Resources.back2;
            this.picSlideShow.Location = new System.Drawing.Point(3, 3);
            this.picSlideShow.Name = "picSlideShow";
            this.picSlideShow.Size = new System.Drawing.Size(617, 318);
            this.picSlideShow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSlideShow.TabIndex = 2;
            this.picSlideShow.TabStop = false;
            this.picSlideShow.Click += new System.EventHandler(this.picSlideShow_Click);
            this.picSlideShow.Paint += new System.Windows.Forms.PaintEventHandler(this.picSlideShow_Paint);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(623, 324);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(0, 350);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(631, 83);
            this.panel5.TabIndex = 2;
=======
            this.mainContent.Size = new System.Drawing.Size(931, 531);
            this.mainContent.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.Location = new System.Drawing.Point(61, 480);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // picSlideShow
            // 
            this.picSlideShow.Image = ((System.Drawing.Image)(resources.GetObject("picSlideShow.Image")));
            this.picSlideShow.Location = new System.Drawing.Point(0, 0);
            this.picSlideShow.Name = "picSlideShow";
            this.picSlideShow.Size = new System.Drawing.Size(927, 531);
            this.picSlideShow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picSlideShow.TabIndex = 1;
            this.picSlideShow.TabStop = false;
            this.picSlideShow.Click += new System.EventHandler(this.picSlideShow_Click);
>>>>>>> 694be5f933a793b567d4c9b92a34c0da9eff153b
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(200)))), ((int)(((byte)(245)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
<<<<<<< HEAD
            this.panel1.Location = new System.Drawing.Point(631, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(4, 433);
=======
            this.panel1.Location = new System.Drawing.Point(927, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(4, 531);
>>>>>>> 694be5f933a793b567d4c9b92a34c0da9eff153b
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown_1);
            this.panel1.MouseEnter += new System.EventHandler(this.panel1_MouseEnter);
            this.panel1.MouseLeave += new System.EventHandler(this.panel1_MouseLeave);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove_1);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp_1);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(200)))), ((int)(((byte)(245)))));
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
<<<<<<< HEAD
            this.panel2.Location = new System.Drawing.Point(0, 467);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(639, 4);
=======
            this.panel2.Location = new System.Drawing.Point(0, 565);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(935, 4);
>>>>>>> 694be5f933a793b567d4c9b92a34c0da9eff153b
            this.panel2.TabIndex = 2;
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseDown);
            this.panel2.MouseEnter += new System.EventHandler(this.panel2_MouseEnter);
            this.panel2.MouseLeave += new System.EventHandler(this.panel2_MouseLeave);
            this.panel2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseMove);
            this.panel2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseUp);
            // 
            // panel4
            // 
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
<<<<<<< HEAD
            this.panel4.Location = new System.Drawing.Point(635, 0);
=======
            this.panel4.Location = new System.Drawing.Point(931, 0);
>>>>>>> 694be5f933a793b567d4c9b92a34c0da9eff153b
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(4, 4);
            this.panel4.TabIndex = 1;
            this.panel4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel4_MouseDown);
            this.panel4.MouseEnter += new System.EventHandler(this.panel4_MouseEnter);
            this.panel4.MouseLeave += new System.EventHandler(this.panel4_MouseLeave);
            this.panel4.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel4_MouseMove);
            this.panel4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel4_MouseUp);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(200)))), ((int)(((byte)(245)))));
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(0, 34);
            this.panel3.Name = "panel3";
<<<<<<< HEAD
            this.panel3.Size = new System.Drawing.Size(4, 433);
=======
            this.panel3.Size = new System.Drawing.Size(4, 531);
>>>>>>> 694be5f933a793b567d4c9b92a34c0da9eff153b
            this.panel3.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
<<<<<<< HEAD
            this.ClientSize = new System.Drawing.Size(639, 471);
=======
            this.ClientSize = new System.Drawing.Size(935, 569);
>>>>>>> 694be5f933a793b567d4c9b92a34c0da9eff153b
            this.Controls.Add(this.mainContent);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.Titlebar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Titlebar.ResumeLayout(false);
            this.mainContent.ResumeLayout(false);
<<<<<<< HEAD
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
=======
>>>>>>> 694be5f933a793b567d4c9b92a34c0da9eff153b
            ((System.ComponentModel.ISupportInitialize)(this.picSlideShow)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

<<<<<<< HEAD
		private System.Windows.Forms.Panel Titlebar;
		private System.Windows.Forms.Button titleClose;
		private System.Windows.Forms.Button titleMin;
		private System.Windows.Forms.Button titleMax;
		private System.Windows.Forms.Panel mainContent;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.PictureBox picSlideShow;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel5;
	}
=======
        private System.Windows.Forms.Panel Titlebar;
        private System.Windows.Forms.Button titleClose;
        private System.Windows.Forms.Button titleMin;
        private System.Windows.Forms.Button titleMax;
        private System.Windows.Forms.Panel mainContent;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox picSlideShow;
        private System.Windows.Forms.Button button1;
    }
>>>>>>> 694be5f933a793b567d4c9b92a34c0da9eff153b
}
