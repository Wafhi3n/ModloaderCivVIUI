using CivLaucherDotNetCore.Vue;
using ModLoader.Vue.ModsViewers;
using ModLoader.Vue.ModsViewers.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
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

namespace ModLoader.View.RightBarMenu
{
    /// <summary>
    /// Logique d'interaction pour UserControl1.xaml
    /// </summary>
    public partial class RightBarMenu : UserControl
    {
        public Back buttonGoMods;

        public SettingsButton sb { get; set; }
        public MainFrame mainFrame { get; set; }
        public string actual { get; set; }

        public List<UserControl> backUC { get; set; }
        public RightBarMenu(MainFrame mainFrame)
        {
            InitializeComponent();
            buttonGoMods = new Back();
            sb = new SettingsButton();


            this.mainFrame = this.mainFrame;
    
            buttonGoMods.BlueArrow.Click += ClickHammer;
            sb.SB.Click += ClickHammer;

            Settings.Content = sb;
            backUC = new List<UserControl>() ;
            actual = "main";
        }

        public void ClickHammer(object sender, RoutedEventArgs e)
        {
            if (actual == "main")
            {

                GoToModFrame(sender, e);
            }
            else
            {
                RetourMainFrame(sender, e);
            }
        }

        private void RetourMainFrame(object sender, RoutedEventArgs e)
        {



                
            if (backUC.Any()) //prevent IndexOutOfRangeException for empty list
            {
                mainFrame.MainContener.Content = backUC.Last();
                backUC.RemoveAt(backUC.Count - 1);
            }
            if(!backUC.Any())
            {
                Settings.Content = sb;
                actual = "main";
            }

            

        }

        private void GoToModFrame(object sender, RoutedEventArgs e)
        {
            if (this.mainFrame.modView != null)
            {
                backUC.Add((UserControl)mainFrame.MainContener.Content);
                mainFrame.MainContener.Content = this.mainFrame.modView;

                actual = "ModsView";
                Settings.Content = buttonGoMods;
            }

        }
        private void QuitButton(object sender, RoutedEventArgs e)
        {
             mainFrame.mainWindow.Close();
        }


    }
}
