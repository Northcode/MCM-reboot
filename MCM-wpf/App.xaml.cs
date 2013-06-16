using MCM.News;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MCM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MainWindow mainWindow;

        App()
        {
            InitializeComponent();
        }

        [STAThread]
        static void Main()
        {
            mainWindow = new MainWindow();
            App app = new App();
            app.Run(mainWindow);
        }

        public static void Log(string Line)
        {
            if (mainWindow != null)
            {
                mainWindow.Dispatcher.Invoke((Action)(() => {
                    mainWindow.txtLog.Text += Line + "\n";
                }));
            }
        }

        public static void InvokeAction(Action a)
        {
            mainWindow.Dispatcher.Invoke(a);
        }
    }
}
