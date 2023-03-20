using CivLaucherDotNetCore.Vue;
using ModLoader.Controller;
using ModLoader.Properties;
using ModloaderClass.Model;
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
    /// Logique d'interaction pour GridItemMod.xaml
    /// </summary>
    public partial class GridItemMod : UserControl
    {
        ModSqliteController modSqliteContoller;
        public GridItemMod(ModSqliteController modSqliteContoller)
        {

            

            this.modSqliteContoller = modSqliteContoller;
            InitializeComponent();
            if (modSqliteContoller.name != null)
            {
                this.nameMod.Content = modSqliteContoller.name;
            }
        }



    }


    public partial class ConfigButton : Button
    {

        public void SetButtonVal(ModSqliteController modSqController,MainFrame mainFrame)
        {

            this.Click += (s, e) =>
            {
                if (!modSqController.isSteam && mainFrame.isGitInstalled)
                {
                    ConfigButtonClick(modSqController, mainFrame);
                    mainFrame.rightBarMenu.backUC.Add(mainFrame.modView);

                }
            };



                if (modSqController.isSteam)
                {
                    SteamButtonSettings();
                }
                else
                {
                    GitButtonSettings(modSqController.Path);
                }





        }
        private void ConfigButtonClick(ModSqliteController m,MainFrame mainFrame)
        {

                m.mGit.getTagsFromRepo();
                mainFrame.MainContener.Content = new ModGitViewer(m, mainFrame);

        }
        private void SteamButtonSettings()
            {

                Style = Application.Current.Resources["settingsSteam"] as Style;
            }

            private void GitButtonSettings(string p)
            {

                if (p == null)
                {
                    Style = Application.Current.Resources["settingsGitRed"] as Style;

                }
                else
                {
                    Style = Application.Current.Resources["settingsGitGreen"] as Style;

                }
            }
        }



    }
