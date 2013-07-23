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
                item.Uid = usr.username + ";" + usr.password;
                lb_users.Items.Add(item);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
                MinecraftUser usr = new MinecraftUser(tb_username.Text, tb_displayname.Text, tb_password.Password);
                MinecraftUserData.users.Add(usr);
                tb_username.Text = "";
            updateList();
        }

        private MinecraftUser getSelectedUser()
        {
            foreach (MinecraftUser usr in MinecraftUserData.users)
            {
                if((lb_users.SelectedItem as ListBoxItem).Uid == usr.username + ";" + usr.password)
                {
                    return usr;
                }
            }
            throw new Exception("Invalid selection");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MinecraftUserData.users.Remove(getSelectedUser());
            updateList();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MinecraftUser user = getSelectedUser();
            tb_displayname.Text = user.displayname;
            tb_username.Text = user.username;
            try
            {
                tb_password.Password = MinecraftUser.decryptPwd(user.password);
            }
            catch
            {
                tb_password.Password = "";
            }
            Button_Click_1(null, null);
        }
    }
}
