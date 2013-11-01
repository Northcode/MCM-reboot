using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MCM.Utils
{
    public partial class Error : Form
    {
        public Error(Exception e)
        {
            InitializeComponent();
            textBox_error.Text = e.Message;
            textBox_stackTrace.Text = e.StackTrace;
            textBox_message.Text = e.Message;           
        }
    }
}
