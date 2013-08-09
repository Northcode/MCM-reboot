using MahApps.Metro.Controls;
using MCM.BackupFramework;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace ModManager
{
    /// <summary>
    /// Interaction logic for ModSelector.xaml
    /// </summary>
    public partial class ModSelector : MetroWindow
    {
        Instance instance;

        public ModSelector(Instance i)
        {
            InitializeComponent();
            this.instance = i;
            this.tabControl.SelectedIndex = 0;
            UpdateList();
        }

        private void UpdateList()
        {
            List<Mod> mods = Main.GetModList(instance);
            listBox_instance.Items.Clear();
            listBox_backup.Items.Clear();

            switch (((tabControl.SelectedItem as TabItem).Tag) as string)
            {
                case "jar":
                    foreach (Mod mod in mods)
                        if (mod.type == Mod.ModType.JarMod) { addItem(mod, false); }
                    foreach (Mod mod in Main.BackuppedMods)
                        if (mod.type == Mod.ModType.JarMod) { addItem(mod, true); }
                    break;
                case "zip":
                    foreach(Mod mod in mods)
                        if (mod.type == Mod.ModType.ZipMod) { addItem(mod,false); }
                    foreach (Mod mod in Main.BackuppedMods)
                        if (mod.type == Mod.ModType.ZipMod) { addItem(mod, true); }
                    break;
                case "dir":
                    foreach (Mod mod in mods)
                        if (mod.type == Mod.ModType.ZipMod) { addItem(mod,false); }
                    foreach (Mod mod in Main.BackuppedMods)
                        if (mod.type == Mod.ModType.ZipMod) { addItem(mod, true); }
                    break;
            }
        }

        private void addItem(Mod mod,bool inBackuppedList)
        {
            ListBoxItem item = new ListBoxItem();
            item.Content = mod.name;
            item.Tag = mod;
            if (inBackuppedList)
                listBox_backup.Items.Add(item);
            else
                listBox_instance.Items.Add(item);
        }

        /// <summary>
        /// Remove mod
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Mod.DeleteMod((listBox_instance.SelectedItem as ListBoxItem).Tag as Mod, instance);
            UpdateList();
        }

        /// <summary>
        /// Add mod
        /// </summary>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Mod.InstallMod(((sender as ListBoxItem).Tag as Mod), instance);
            UpdateList();
        }

        /// <summary>
        /// Delete Backup
        /// </summary>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Mod mod = ((sender as ListBoxItem).Tag as Mod);
            Main.BackuppedMods.Remove(mod);
            if (File.Exists(mod.path))
                File.Delete(mod.path);
            if (Directory.Exists(mod.path))
                Directory.Delete(mod.path,true);
            UpdateList();
        }

        /// <summary>
        /// Add Backup
        /// </summary>
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
