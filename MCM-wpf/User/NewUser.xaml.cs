using MahApps.Metro.Controls;
using MCM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MCM.User
{
    /// <summary>
    /// Interaction logic for NewUser.xaml
    /// </summary>
    public partial class NewUser : MetroWindow
    {
        public bool isEdit = false;

        public NewUser()
        {
            InitializeComponent();
            updateList();
        }

        private void updateList()
        {
            lb_users.Items.Clear();
            foreach (MinecraftUser usr in MinecraftUserData.users)
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = usr.displayname;
                lb_users.Items.Add(item);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isEdit)
            {

            }
            else
            {
                MinecraftUser usr = new MinecraftUser(tb_username.Text, tb_displayname.Text, tb_password.Text);
                MinecraftUserData.users.Add(usr);
            }
            updateList();
        }
    }
}
