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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Titlebar = new System.Windows.Forms.Panel();
            this.titleMin = new System.Windows.Forms.Button();
            this.titleMax = new System.Windows.Forms.Button();
            this.titleClose = new System.Windows.Forms.Button();
            this.mainContent = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
<<<<<<< HEAD
            this.lblTitle = new System.Windows.Forms.Label();
=======
            this.picSlideShow = new System.Windows.Forms.PictureBox();
>>>>>>> dev
            this.Titlebar.SuspendLayout();
            this.mainContent.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSlideShow)).BeginInit();
            this.SuspendLayout();
            // 
            // Titlebar
            // 
            this.Titlebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(200)))), ((int)(((byte)(245)))));
            this.Titlebar.Controls.Add(this.lblTitle);
            this.Titlebar.Controls.Add(this.titleMin);
            this.Titlebar.Controls.Add(this.titleMax);
            this.Titlebar.Controls.Add(this.titleClose);
            this.Titlebar.Dock = System.Windows.Forms.DockStyle.Top;
            this.Titlebar.Location = new System.Drawing.Point(0, 0);
            this.Titlebar.Name = "Titlebar";
            this.Titlebar.Size = new System.Drawing.Size(935, 34);
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
            this.titleMin.Location = new System.Drawing.Point(821, 0);
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
            this.titleMax.Location = new System.Drawing.Point(858, 0);
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
            this.titleClose.Location = new System.Drawing.Point(891, 0);
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
            this.mainContent.Controls.Add(this.tabControl1);
            this.mainContent.Controls.Add(this.panel5);
            this.mainContent.Controls.Add(this.panel1);
            this.mainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainContent.Location = new System.Drawing.Point(4, 34);
            this.mainContent.Name = "mainContent";
<<<<<<< HEAD
            this.mainContent.Size = new System.Drawing.Size(635, 416);
=======
            this.mainContent.Size = new System.Drawing.Size(931, 531);
>>>>>>> dev
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
<<<<<<< HEAD
            this.tabControl1.Size = new System.Drawing.Size(631, 333);
=======
            this.tabControl1.Size = new System.Drawing.Size(927, 448);
>>>>>>> dev
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.picSlideShow);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
<<<<<<< HEAD
            this.tabPage1.Size = new System.Drawing.Size(623, 307);
=======
            this.tabPage1.Size = new System.Drawing.Size(919, 422);
>>>>>>> dev
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
<<<<<<< HEAD
            // picSlideShow
            // 
            this.picSlideShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picSlideShow.Image = global::MCM_reboot.Properties.Resources.back2;
            this.picSlideShow.Location = new System.Drawing.Point(3, 3);
            this.picSlideShow.Name = "picSlideShow";
            this.picSlideShow.Size = new System.Drawing.Size(617, 301);
            this.picSlideShow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picSlideShow.TabIndex = 2;
            this.picSlideShow.TabStop = false;
            this.picSlideShow.Click += new System.EventHandler(this.picSlideShow_Click);
            this.picSlideShow.Paint += new System.Windows.Forms.PaintEventHandler(this.picSlideShow_Paint);
            // 
=======
>>>>>>> dev
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(919, 422);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
<<<<<<< HEAD
            this.panel5.Location = new System.Drawing.Point(0, 333);
=======
            this.panel5.Location = new System.Drawing.Point(0, 448);
>>>>>>> dev
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(927, 83);
            this.panel5.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(200)))), ((int)(((byte)(245)))));
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(927, 0);
            this.panel1.Name = "panel1";
<<<<<<< HEAD
            this.panel1.Size = new System.Drawing.Size(4, 416);
=======
            this.panel1.Size = new System.Drawing.Size(4, 531);
>>>>>>> dev
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
            this.panel2.Location = new System.Drawing.Point(0, 450);
=======
            this.panel2.Location = new System.Drawing.Point(0, 565);
>>>>>>> dev
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(935, 4);
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
            this.panel4.Location = new System.Drawing.Point(931, 0);
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
            this.panel3.Size = new System.Drawing.Size(4, 416);
            this.panel3.TabIndex = 3;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(7, 5);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(110, 22);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "MC Manager";
=======
            this.panel3.Size = new System.Drawing.Size(4, 531);
            this.panel3.TabIndex = 3;
            // 
            // picSlideShow
            // 
            this.picSlideShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picSlideShow.Image = global::MCM_reboot.Properties.Resources.back2;
            this.picSlideShow.Location = new System.Drawing.Point(3, 3);
            this.picSlideShow.Name = "picSlideShow";
            this.picSlideShow.Size = new System.Drawing.Size(913, 416);
            this.picSlideShow.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picSlideShow.TabIndex = 0;
            this.picSlideShow.TabStop = false;
            this.picSlideShow.Paint += new System.Windows.Forms.PaintEventHandler(this.picSlideShow_Paint);
>>>>>>> dev
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
<<<<<<< HEAD
            this.ClientSize = new System.Drawing.Size(639, 454);
=======
            this.ClientSize = new System.Drawing.Size(935, 569);
>>>>>>> dev
            this.Controls.Add(this.mainContent);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.Titlebar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Titlebar.ResumeLayout(false);
            this.Titlebar.PerformLayout();
            this.mainContent.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSlideShow)).EndInit();
            this.ResumeLayout(false);

        }   

        #endregion

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
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel5;
<<<<<<< HEAD
        private System.Windows.Forms.Label lblTitle;
=======
        private System.Windows.Forms.PictureBox picSlideShow;
>>>>>>> dev
	}
}
