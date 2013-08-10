using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MCM.Utils;

namespace ModManager
{
    /// <summary>
    /// Interaction logic for NewMod.xaml
    /// </summary>
    public partial class NewMod : MetroWindow
    {
        public NewMod()
        {
            InitializeComponent();
        }

        public void comboBox_vType_selectionChanged(object sender, EventArgs e)
        {

        }

        public void tabControl_modType_selectionChanged(object sender, EventArgs e)
        {
            tb_path.Text = "";
        }

        public void bt_browse_Click(object sender, EventArgs e)
        {
            if ((tabControl_modType.SelectedItem as TabItem).Tag.ToString() == "zip")
            {
                System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
                dialog.Filter = "Zipped mod|*.zip";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    tb_path.Text = dialog.FileName;
                }
            }
            else
            {
                FolderSelectDialog fsd = new FolderSelectDialog();
                fsd.ShowDialog();
                if (fsd.ShowDialog())
                {
                    tb_path.Text = fsd.FileName;
                }
            }
            /*
            string path = null;

            if ((((tabControl.SelectedItem as TabItem).Tag) as string) == "zip")
            {
                System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
                dialog.Filter = "Zipped mod|*.zip";
                if (dialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;
                else
                    path = dialog.FileName;
            }
            */
        }
    }
}
