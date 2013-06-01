using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCM_reboot
{
	public partial class Form1 : Form
	{
		

		public Form1()
		{
			InitializeComponent();
		}

		#region customWindowStuff
		MouseEventArgs mlast;
		bool mmoving = false;
		bool mresizeingh = false;
		bool mresizeingw = false;
		bool mresizeingwh = false;

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
			if (mresizeingw)
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
			if (mresizeingh)
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
			if (mresizeingwh)
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
	}
}
