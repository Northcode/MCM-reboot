using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace MCM.PluginAPI
{
    public interface IBackup
    {
        string Name { get; }

        TreeViewItem treeItem { get; }
    }
}
