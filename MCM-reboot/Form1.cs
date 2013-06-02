using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCM_reboot
{
	public partial class Form1 : Form
	{
		#region ImageSlideShow

		Timer fadeTimer;
		int lightCounter;
		Color fadeTo;
		Image img;

		void FadeImage()
		{
			fadeTimer.Start();
		}

		void fadeTick(object sender, EventArgs e)
		{
			if (lightCounter == 100)
			{
				lightCounter = 50;
				fadeTimer.Stop();
                picSlideShow.Image = MCM_reboot.Properties.Resources.back2;
			}
			else
			{
				picSlideShow.Image = LightenImg(lightCounter++, fadeTo.R, fadeTo.G, fadeTo.B, picSlideShow.Image);
			}
		}

		Image LightenImg(int level,int nRed, int nGreen, int nBlue, Image imgLight)
		{
			//convert image to graphics object
			Graphics graphics = Graphics.FromImage(imgLight);
			int conversion = (5 * (level - 50)); //calculate new alpha value
			//create mask with blended alpha value and chosen color as pen 
			Pen pLight = new Pen(Color.FromArgb(conversion, nRed,
								 nGreen, nBlue), imgLight.Width * 2);
			//apply created mask to graphics object
			graphics.DrawLine(pLight, -1, -1, imgLight.Width, imgLight.Height);
			//save created graphics object and modify image object by that
			graphics.Save();
			graphics.Dispose(); //dispose graphics object
			return imgLight; //return modified image
		}

		#endregion

		public Form1()
		{
			InitializeComponent();
			fadeTimer = new Timer();
			fadeTimer.Interval = 50;
			fadeTimer.Tick += fadeTick;
			lightCounter = 50;
		}

		#region customWindowStuff
		MouseEventArgs mlast;
		bool mmoving = false;
		bool mresizeingh = false;
		bool mresizeingw = false;
		bool mresizeingwh = false;

		public bool Resizeable { get; set; }

		private void button1_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void panel1_MouseMove(object sender, MouseEventArgs e)
		{
			if (mmoving)
			{
				this.Top += e.Y - mlast.Y;
				this.Left += e.X - mlast.X;
			}
		}

		private void panel1_MouseDown(object sender, MouseEventArgs e)
		{
			mlast = e;
			mmoving = true;
		}

		private void panel1_MouseUp(object sender, MouseEventArgs e)
		{
			mmoving = false;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (this.WindowState == FormWindowState.Normal)
			{
				this.WindowState = FormWindowState.Maximized;
				(sender as Button).Text = "v";
			}
			else if (this.WindowState == FormWindowState.Maximized)
			{
				this.WindowState = FormWindowState.Normal;
				(sender as Button).Text = "^";
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Minimized;
		}

		private void panel1_MouseDown_1(object sender, MouseEventArgs e)
		{
			mlast = e;
			mresizeingw = true;
		}

		private void panel1_MouseUp_1(object sender, MouseEventArgs e)
		{
			mresizeingw = false;
		}

		private void panel1_MouseMove_1(object sender, MouseEventArgs e)
		{
			if (mresizeingw && Resizeable)
			{
				this.Width += e.X - mlast.X;
			}
		}

		private void panel2_MouseDown(object sender, MouseEventArgs e)
		{
			mlast = e;
			mresizeingh = true;
		}

		private void panel2_MouseMove(object sender, MouseEventArgs e)
		{
			if (mresizeingh && Resizeable)
			{
				this.Height += e.Y - mlast.Y;
			}
		}

		private void panel2_MouseUp(object sender, MouseEventArgs e)
		{
			mresizeingh = false;
		}

		private void panel2_MouseEnter(object sender, EventArgs e)
		{
			this.Cursor = Cursors.SizeNS;
		}

		private void panel2_MouseLeave(object sender, EventArgs e)
		{
			this.Cursor = Cursors.Arrow;
		}

		private void panel1_MouseEnter(object sender, EventArgs e)
		{
			this.Cursor = Cursors.SizeWE;
		}

		private void panel1_MouseLeave(object sender, EventArgs e)
		{
			this.Cursor = Cursors.Arrow;
		}

		private void panel4_MouseDown(object sender, MouseEventArgs e)
		{
			mresizeingwh = true;
			mlast = e;
		}

		private void panel4_MouseEnter(object sender, EventArgs e)
		{
			this.Cursor = Cursors.SizeNWSE;
		}

		private void panel4_MouseLeave(object sender, EventArgs e)
		{
			this.Cursor = Cursors.Arrow;
		}

		private void panel4_MouseMove(object sender, MouseEventArgs e)
		{
			if (mresizeingwh && Resizeable)
			{
				this.Height += e.Y - mlast.Y;
				this.Width += e.X - mlast.X;
			}
		}

		private void panel4_MouseUp(object sender, MouseEventArgs e)
		{
			mresizeingwh = false;
		}

		#endregion

		bool a = false;

		private void button1_Click_1(object sender, EventArgs e)
		{
			fadeTo = (a ? Color.Black : Color.White);
			fadeTimer.Start();
			a = !a;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			img = picSlideShow.Image;
            Resizeable = true;
		}

		private void picSlideShow_Click(object sender, EventArgs e)
		{
			img = picSlideShow.Image.Clone() as Image;
		}

        private void picSlideShow_Paint(object sender, PaintEventArgs e)
        {
            LinearGradientBrush lgb = new LinearGradientBrush(picSlideShow.ClientRectangle, Color.FromArgb(0,Color.White), Color.White, 3f, true);
            e.Graphics.FillRectangle(lgb, picSlideShow.ClientRectangle);
            lgb.Dispose();
        }
	}
}
