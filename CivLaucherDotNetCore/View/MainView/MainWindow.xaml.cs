using CivLaucherDotNetCore.Vue;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModLoader.Controller;
using ModLoader.Utils;
using ModLoader.Vue;
using ModloaderClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace CivLauncher
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    /// 



    public partial class MainWindow : Window
    {

        //MainWindowViewModel mainView;
        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Content = new MainFrame(this);


        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
    
}

