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
using MCM.MinecraftFramework;

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
            AddItems();
            //comboBox_vType_selectionChanged(null, null);
        }

        private void AddItems()
        {
            foreach (TinyMinecraftVersion ver in VersionManager.versions)
            {
                comboBox_mcver.Items.Add(new ComboBoxItem()
                {
                    Content = ver.Key,
                    Tag = ver
                });
            }
        }

        public void comboBox_vType_selectionChanged(object sender, EventArgs e)
        {
            ReleaseType type = ReleaseType.unknown;
            string value = ((comboBox_vType.SelectedItem as ComboBoxItem).Content.ToString());
            switch (value)
            {
                case "Release":
                    type = ReleaseType.release;
                    break;
                case "Snapshot":
                    type = ReleaseType.snapshot;
                    break;
                case "Beta":
                    type = ReleaseType.old_beta;
                    break;
                case "Alpha":
                    type = ReleaseType.old_alpha;
                    break;
                case "All":
                    comboBox_mcver.Items.Filter = null;
                    return;
            }

            comboBox_mcver.Items.Filter = (p) => { return ((p as Control).Tag as TinyMinecraftVersion).Type == type; };


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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tb_path.Text != "" && tb_name.Text != "" && comboBox_mcver.SelectedIndex != -1)
            {
                Mod mod = new Mod();
                switch (tabControl_modType.SelectedIndex)
                {
                    case 0:
                        mod.type = Mod.ModType.ZipMod;
                        break;
                    case 1:
                        mod.type = Mod.ModType.DirMod;
                        break;
                    case 2:
                        mod.type = Mod.ModType.JarMod;
                        break;
                }
                mod.name = tb_name.Text;
                mod.level = Mod.ModLevel.mod;
                mod.version = (comboBox_mcver.SelectedItem as ComboBoxItem).Tag as TinyMinecraftVersion;

                string target;
                if (mod.type != Mod.ModType.ZipMod)
                {
                    target = Main.DataPath + "\\" + new System.IO.DirectoryInfo(tb_path.Text).Name;
                    MCM.Utils.CopyDirectory.Copy(tb_path.Text, target);
                }
                else
                {
                    target = Main.DataPath + "\\" + System.IO.Path.GetFileName(tb_path.Text);
                    System.IO.File.Copy(tb_path.Text, target);
                }
                mod.path = target;
                Main.BackuppedMods.Add(mod);
                this.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
