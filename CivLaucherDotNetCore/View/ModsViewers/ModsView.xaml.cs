using CivLaucherDotNetCore.Vue;
using CivLauncher;
using ModLoader.Controller;
using ModLoader.Utils;
using ModLoader.View.ModsViewers.ModView;
using ModLoader.View.RightBarMenu;
using ModLoader.View.ScrollBar;
using ModLoader.Vue.ModsViewers.Model;
using ModloaderClass;
using ModloaderStyle;
using ModloaderStyle.ModLoaderButton;
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

namespace ModLoader.Vue.ModsViewers
{
    /// <summary>
    /// Logique d'interaction pour ModsView.xaml
    /// </summary>
    public partial class ModsView : UserControl
    {

        MainFrame mainFrame;
        ContentControl mainContentControl;
        BankSqliteModsController bmsc;
        RightBarMenu Rbm;
        public ModGitViewer modGitViewers { get; set; } 
        public ModsView(MainFrame mainFrame)
        {
            this.mainFrame = mainFrame;
            InitializeComponent();


            mainFrame.bankSqlite.AddViewToGridView(this);


            //DataGridMod.ItemsSource = bmsc.OMod;



        }



        public void addModToGrid(ModSqliteController m, BankSqliteModsController bmsc)
        {
            int index = bmsc.mods.FindIndex(a => a.Id == m.Id);

            GridItemMod gimg = new GridItemMod(m);



            //AddGridModRow();


            gimg.CB.SetButtonVal(m, mainFrame);









            RowDefinition rowDef1 = new RowDefinition();
            GridMod.RowDefinitions.Add(rowDef1);



            GridMod.Children.Add(gimg);
            Grid.SetRow(gimg, index+1);



        }
        public void AddGridModCollum()
        {
            ColumnDefinition colDef1 = new ColumnDefinition();
            GridMod.ColumnDefinitions.Add(colDef1 as ColumnDefinition);
        }
        public void AddGridModRow()
        {
            RowDefinition rowDef1 = new RowDefinition();
            //rowDef1.Height = new GridLength(80);
            GridMod.RowDefinitions.Add(rowDef1 as RowDefinition);
        }




        private void ComboBox_SelectionChanged(object sender, EventArgs e)
        {
            ModView mv = ((ModView)(((ComboBox)sender).DataContext));

            String g = (String)(((ComboBox)sender).SelectedItem);

            mv.tagSelect = g;


            if (mv.tagCanChange(g))
            {
                mv.changeVisibilityButtonAndStatus(true);
            }
            else
            {
                mv.changeVisibilityButtonAndStatus(false);

            }
        }


    }
}
