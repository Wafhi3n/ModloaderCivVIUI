using Accessibility;
using CivLauncher;
using ModLoader.Controller;
using ModLoader.View.ModsViewers.ModView;
using ModLoader.View.ScrollBar;
using ModLoader.Vue;
using ModLoader.Vue.ModsViewers;
using ModLoader.Vue.ModsViewers.Model;
using ModloaderClass;
using ModloaderClass.Model;
using ModloaderStyle;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml.Linq;

namespace CivLaucherDotNetCore.Vue
{
    /// <summary>
    /// Logique d'interaction pour ModsViewer.xaml
    /// </summary>
    public partial class ModGitViewer : UserControl
    {

        MainFrame mainFrame;
        ModSqliteController m;
        private ObservableCollection<ModView> OMod;
        public ModGitViewer(ModSqliteController m, MainFrame mainFrame)
        {
            InitializeComponent();
            this.m= m;
            this.mainFrame=mainFrame;

            nameMod.Content = m.vue.name;
            tag.Content = m.vue.tag;
            selectTag.SelectedItem=m.vue.tag;



 


            selectTag.ItemsSource = m.vue.tags;
            selectTag.SelectionChanged += ComboBox_SelectionChanged;
            buttonAction.Click += update_Click;
            //selectTag.

            /*
            HeaderDisplayGitMod Header = new HeaderDisplayGitMod();

            Grid.SetColumn(Header.name, 0);
            Grid.SetColumn(Header.tag, 1);
            Grid.SetColumn(Header.lasTag, 2);
            Grid.SetColumn(Header.majDispo, 3);

            Grid.SetRow(Header.name, 1);
            Grid.SetRow(Header.lasTag, 1);
            Grid.SetRow(Header.majDispo, 1);
            Grid.SetRow(Header.tag, 1);


            DisplayGitMod.Children.Add(Header.name);
            DisplayGitMod.Children.Add(Header.tag);
            DisplayGitMod.Children.Add(Header.majDispo);
            DisplayGitMod.Children.Add(Header.lasTag);





            //DisplayGitMod.Children.Add(Header);
            //DisplayGitMod.Margin = new Thickness(20,30,20,20);



            GridItemModGit gg = new GridItemModGit(m);

            Grid.SetColumn(gg.NameText, 0);
            Grid.SetColumn(gg.Tag, 1);
            Grid.SetColumn(gg.LastTag, 2);
            Grid.SetColumn(gg.selectTag, 3);
            Grid.SetColumn(gg.buttonAction, 4);

            Grid.SetRow(gg.NameText, 1);
            Grid.SetRow(gg.LastTag, 1);
            Grid.SetRow(gg.Tag, 1);
            Grid.SetRow(gg.selectTag, 1);
            Grid.SetRow(gg.buttonAction, 1);


            DisplayGitMod.Children.Add(gg.NameText);
            DisplayGitMod.Children.Add(gg.Tag);
            DisplayGitMod.Children.Add(gg.LastTag);
            DisplayGitMod.Children.Add(gg.selectTag);
            DisplayGitMod.Children.Add(gg.buttonAction);

            gg.selectTag.SelectionChanged+=ComboBox_SelectionChanged;
            gg.buttonAction.Click += update_Click;

            */



            //DisplayGitMod.ItemsSource = m.vue;


            //selectlanguages.Content = new SelectLanguages(view);
            //DataGridMod.ItemsSource = OMod;
        }

        public void AddGridModRow(Grid g)
        {
            RowDefinition rowDef1 = new RowDefinition();
            g.RowDefinitions.Add(rowDef1 as RowDefinition);
        }
        private async void update_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as System.Windows.Controls.Button;
            m.vue.updateBranchToTagClickAsync();
        }


        private void ComboBox_SelectionChanged(object sender, EventArgs e)
        {
            System.Windows.Controls.ComboBox cb = sender as System.Windows.Controls.ComboBox;

            //Console.WriteLine(cb);
            //cb.GetValue();
            //ModView mv = ((ModView)(((System.Windows.Controls.ComboBox)sender).DataContext));


         //   'ModLoader.Vue.MainWindowViewModel' to type 'ModLoader.Vue.ModsViewers.Model.ModView'.'

            String g = (String)cb.SelectedItem;

            m.vue.tagSelect = g;


            if (m.vue.tagCanChange(g))
            {
                m.vue.changeVisibilityButtonAndStatus(true);
            }
            else
            {
                m.vue.changeVisibilityButtonAndStatus(false);

            }
        }
    }
}
