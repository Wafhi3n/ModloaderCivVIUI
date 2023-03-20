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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ModLoader.View.ScrollBar
{
    /// <summary>
    /// Logique d'interaction pour Announcer.xaml
    /// </summary>
    public partial class Announcer : UserControl
    {

        public Announcer()
        {
            InitializeComponent();
            /*this.canMain = _canMain;
            this.textBlock = textBlock;*/
            labelInfo = new TextBlock();
            
        }
        public string labelInfoV
        {
            get
            { return labelInfo.Text; }
            set
            {
                //if (textBlock.Text != value)
                //{
                labelInfo.Text = value;
                ScrollLabelInfo();
                //}
            }
        }
        public void ScrollLabelInfo()
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = -labelInfo.ActualWidth;
            doubleAnimation.To = _canMain.ActualWidth;
            doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
            doubleAnimation.Duration = new Duration(TimeSpan.Parse("0:0:30"));
            labelInfo.BeginAnimation(Canvas.LeftProperty, doubleAnimation);
        }

        public void setLabelUpdate(BankGitModController bmc)
        {
            bmc.DisplayisUpdateAviable();
            //setTextUpdateAviable(lmc);
        }
        public void setTextUpdateAviable(string a)
        {
            if (labelInfoV == "Bon Jeu" || labelInfoV == null)
            {
                labelInfoV = a;
            }
            else
            {
                labelInfoV += a;
                ScrollLabelInfo();

            }

        }
    }
}
