using ModLoader.Controller;
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

namespace ModLoader.View.ModsViewers.ModView
{
    /// <summary>
    /// Logique d'interaction pour GridItemModGit.xaml
    /// </summary>
    public partial class GridItemModGit : UserControl
    {
        ModGitController mg;
        public GridItemModGit(ModGitController mg)
        {
            this.mg = mg;
            InitializeComponent();
        }
    }
}
