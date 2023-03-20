using CivLauncher;
using ModLoader.Controller;
using ModLoader.Utils;
using ModLoader.View.ScrollBar;
using ModloaderClass;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ModLoader.Vue.ModsViewers.Model
{
    public enum ButtonAction
    {
        install = 0,
        maj = 1,
        rien = 2
    }
    public class ModView : INotifyPropertyChanged
    {
        public ModGitController modP { set; get; }
        public ModSqliteController moSP { set; get; }
        public event PropertyChangedEventHandler PropertyChanged;
        public Announcer st { set; get; }

        private string buttonViewModVal;


        System.Windows.Visibility visibility;

        public System.Windows.Visibility buttonUpdateVisible
        {
            get
            {
                return visibility;

            }
            set
            {
                visibility = value;
                NotifyPropertyChanged();
            }
        }

        public string buttonViewMod
        {
            get
            {
                return buttonViewModVal;
            }
            set
            {
                if (buttonViewModVal != value)
                {
                    buttonViewModVal = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string tagSelectVal;
        public string tagSelect
        {
            get
            {
                return tagSelectVal;
            }
            set
            {
                if (tagSelectVal != value)
                {
                    tagSelectVal = value;
                }
                NotifyPropertyChanged();
            }
        }
        public ModGitController Model
        {

            get { return modP; }

            set
            {
                if (modP != value)
                {
                    modP = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool tagCanChange(string g)
        {

            string a = modP.tag;
            string b = tagSelect;
            return a != b;
        }

        internal void changeVisibilityButtonAndStatus(bool v)
        {

            if (v)
            {
                buttonUpdateVisible = System.Windows.Visibility.Visible;

                if (!modP.isInstalled())
                {
                    changeButtonMod(ButtonAction.install);
                    // button to install
                }
                else
                {
                    changeButtonMod(ButtonAction.maj);
                    //button to maj
                }


            }
            else
            {
                buttonUpdateVisible = System.Windows.Visibility.Hidden;
            }


        }


        public ModView(ModGitController mod)
        {
            moSP = null;
            modP = mod;
            tags = new ObservableCollection<string>();
            changeVisibilityButtonAndStatus(false);



        }
        public ModView(ModSqliteController mod)
        {
            moSP = mod;
            tags = new ObservableCollection<string>();
            changeVisibilityButtonAndStatus(false);
        }





        public void InitializeModView()
        {
            if (modP != null && modP.isInstalled())
            {
                lastag = modP.lastag;
                tagSelect = modP.tag;


                if (modP.GetTags().Count > 0)
                {
                    if (tagSelect != modP.GetTags().First())
                    {
                        changeButtonMod(ButtonAction.maj);

                    }
                    else
                    {
                        changeButtonMod(ButtonAction.rien);

                    }
                }

            }
            else
            {
                changeButtonMod(ButtonAction.install);
            }
        }








        public void changeButtonMod(ButtonAction a)
        {

            //localiser
            switch (a)
            {
                case ButtonAction.install:
                    buttonViewMod = "Installer le mod";
                    break;
                case ButtonAction.rien:
                    buttonViewMod = "";
                    break;
                case ButtonAction.maj:
                    buttonViewMod = "Changer de version";
                    break;
            }



        }
        //public ModController ModController { set;  get; }

        public string nameValue { set; get; }
        public string name
        {
            get
            {
                return moSP.name;
            }

            set
            {
                moSP.name = value;
                NotifyPropertyChanged();
            }
        }

        public string tag
        {
            get
            {
                return modP.tag;
            }

            set
            {
                modP.tag = value;
                NotifyPropertyChanged();
            }
        }



        public string repoName
        {
            get
            {
                return modP.depot;
            }

            set
            {
                modP.depot = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<string> tags { get; set; }

        public string lastag
        {
            get
            {
                return modP.lastag;
            }

            set
            {

                modP.lastag = value;
                NotifyPropertyChanged();
            }
        }

        public Boolean isSteam
        {
            get
            {
                return moSP.isSteam;
            }

            set
            {

                moSP.isSteam = value;
                NotifyPropertyChanged();
            }
        }





        private string labelInfoValue;
        public string labelInfo
        {
            get
            {
                return labelInfoValue;
            }

            set
            {

                labelInfoValue = value;
                NotifyPropertyChanged();
            }
        }
        public void  updateBranchToTagClickAsync()
        {
            if (tagSelect != null)
            {
                GitController.updateBranchToTagAsync(modP, tagSelect);
                st.labelInfoV = "";
                st.setTextUpdateAviable(InfoLabelModInstall());
                st.ScrollLabelInfo();
                changeVisibilityButtonAndStatus(false);

            }
            else
            {
                if (!modP.isInstalled())
                {
                    //await ModController.cloneMod();

                    tagSelect = modP.tag;
                    lastag = modP.lastag;
                    st.labelInfoV = "";
                    st.setTextUpdateAviable(InfoLabelModInstall());
                    st.ScrollLabelInfo();
                    changeVisibilityButtonAndStatus(false);
                }


            }
        }

        public string InfoLabelModInstall()
        {
            return repoName + " " + modP.tag + " " + "Installé";



        }

        public string InfoLabelModCanUpdate()
        {
            string a = "...Mise à jour de " + modP.depot + " " + modP.tag + " vers " + lastag + " disponible...";
            return a;
        }

    }
}

