﻿using CivLaucherDotNetCore.Controleur;
using CivLaucherDotNetCore.Vue.Model;
using CivLauncher;
using ModLoader.Translation;
using ModLoader.Vue;
using ModloaderClass;
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

namespace CivLaucherDotNetCore.Vue
{
    /// <summary>
    /// Logique d'interaction pour ModsViewer.xaml
    /// </summary>
    public partial class ModsViewer : UserControl
    {
        BankModController bmc;
        MainFrame mainFrame;
        ContentControl mainContentControl;
        ScrollText contentControlLabelInfo;
        MainWindowViewModel mainView;


        private ObservableCollection<ModView> OMod;
        //ComboBox Tagscb;
        public ModsViewer(Controleur.BankModController bmc, MainFrame mainFrame, ContentControl mainContentControl, ScrollText contentControlLabelInfo/*MainWindowViewModel view*/)
        {
            InitializeComponent();
            //mainView = view;
            this.contentControlLabelInfo = contentControlLabelInfo;
            OMod = new ObservableCollection<ModView>();

            foreach( ModController m in bmc.modsController)
            {
                OMod.Add(new ModView(m, contentControlLabelInfo));
            }
            
            this.bmc=bmc;
            this.mainFrame=mainFrame;
            this.mainContentControl=mainContentControl;
            //selectlanguages.Content = new SelectLanguages(view);
            DataGridMod.ItemsSource = OMod;
        }
        private async void update_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            await ((ModView)(button.DataContext)).updateBranchToTagClickAsync();
        }


        private void RetourMainFrame(object sender, RoutedEventArgs e)
        {
            if (this.mainFrame != null)
            {
                this.mainContentControl.Content = this.mainFrame;
            }

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



            // Do actions
        }
    }
}
