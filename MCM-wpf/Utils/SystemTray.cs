using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MCM.Utils
{
    public class SystemTray
    {
        private NotifyIcon notifyIcon;
        private ContextMenu contextMenu;
        private System.ComponentModel.IContainer components;
        private MenuItem menu_startMinecraft;
        private MenuItem menu_exit;

        public SystemTray()
        {
            menu_startMinecraft = new MenuItem();
            menu_startMinecraft.Text = "Start Minecraft";
            menu_startMinecraft.Click += delegate
            {
                App.InvokeAction(delegate
                {
                    App.mainWindow.StartMinecraftButton(null, null);
                });
            };

            menu_exit = new MenuItem();
            menu_exit.Text = "Exit";
            menu_exit.Click += delegate
            {
                App.InvokeAction(delegate
                {
                    App.mainWindow.Close();
                });
            };

            this.contextMenu = new ContextMenu();
            this.contextMenu.MenuItems.AddRange(new MenuItem[] {menu_startMinecraft,menu_exit});

            this.components = new System.ComponentModel.Container();

            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyIcon.Icon = MCM.Properties.Resources.favicon;
            this.notifyIcon.Visible = true;
            this.notifyIcon.ContextMenu = contextMenu;
            this.notifyIcon.Click += notifyIcon_Click;
        }

        void notifyIcon_Click(object sender, EventArgs e)
        {
            App.InvokeAction(delegate
            {
                App.mainWindow.Focus();
            });
        }
    }
}
