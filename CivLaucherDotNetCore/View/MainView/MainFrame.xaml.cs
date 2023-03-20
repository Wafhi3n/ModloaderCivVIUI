using CivLauncher;
using Microsoft.Web.WebView2.Core;
using ModLoader.Controller;
using ModLoader.Vue;
using ModLoader.Vue.ModsViewers;
using ModloaderStyle.ModLoaderButton;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using ModloaderStyle;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using ModLoader.Utils;
using Microsoft.Extensions.DependencyInjection;
using ModLoader.View.ScrollBar;
using ModLoader.View.RightBarMenu;
using ModLoader.Properties;
using ModloaderClass.Model;
using System.Security.Cryptography;

namespace CivLaucherDotNetCore.Vue
{
    /// <summary>
    /// Logique d'interaction pour MainFrame.xaml
    /// </summary>
    public partial class MainFrame : UserControl
    {
        //ModsGitViewer modGitViewers;
        public Boolean isGitInstalled{ get; set; }
        public ModsView modView { get; set; }
        public Announcer announcer { get; set; }
        public BankGitModController bankgit { get; set; } 
        public BankSqliteModsController bankSqlite { get; set; }
        public RightBarMenu rightBarMenu { get; set; }
        public MainWindow mainWindow { get; set; }
        public WebView web { get; set; }


       // public CoreWebView2Environment web { get; set; }
        public ModGitViewer modGitViewer { get; set; }

        public MainFrame(MainWindow mainWindow)
        {
            //mainView = view;

            this.isGitInstalled = GitController.CheckInstallGit();

            if (!isGitInstalled)
            {
                GitController.InstallGit();
            }

            this.mainWindow = mainWindow;




            InitializeComponent();
            announcer = new Announcer();
            Scrollbar.Content = announcer;
            this.bankSqlite = new BankSqliteModsController(this);

            if (isGitInstalled)
            {
                this.bankgit = new BankGitModController(this);
            }

            this.modView = new ModsView(this);
            web =  new WebView();



            MainContener.Content = web;

            rightBarMenu = new RightBarMenu(this);
            rightBarMenu.mainFrame = this;
            RightMenu.Content = rightBarMenu;






            Button bp = ModLoaderButton.ButtonPlay(button_Click);
            GridButtonPlay.Children.Add(bp);

            //IServiceScope services = Services.Service.CreateScope();
            //LocalizationModloader loc = services.ServiceProvider.GetRequiredService<LocalizationModloader>();



            //bJouer.Style = BoutonJouer;
            //this.st.labelInfoV = "Bon jeu";
            //this.modGitViewers = new ModsGitViewer(bmc, bmsc, this, mainContentControl, st);


        }
        private void button_Click(object sender, RoutedEventArgs e)
        {

            Shorcut s = new Shorcut();

            var process = new Process();
            process.StartInfo.UseShellExecute = true;

            process.StartInfo.FileName =s.GetSteamX12Dir();


            process.Start();





        }

       public void MainFrame_Loaded(object sender, RoutedEventArgs e)
        {
            this.announcer = new Announcer();
            if (announcer != null){
            this.announcer.ScrollLabelInfo();
          }
        }

    }
}
