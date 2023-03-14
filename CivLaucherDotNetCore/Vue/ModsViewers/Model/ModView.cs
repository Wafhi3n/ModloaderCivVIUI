using CivLaucherDotNetCore.Controleur;
using CivLauncher;
using ModLoader.Utils;
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

namespace CivLaucherDotNetCore.Vue.Model
{
    public enum ButtonAction
    {
        install = 0,
        maj = 1,
        rien = 2
    }
    public class ModView : INotifyPropertyChanged
    {
        public ModController modP { set; get; }
        public event PropertyChangedEventHandler PropertyChanged;
        public ScrollText st { set; get; }

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
                this.visibility = value;
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
                if (this.buttonViewModVal != value)
                {
                    this.buttonViewModVal = value;
                    NotifyPropertyChanged();
                }
            }
        }


        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private String tagSelectVal;
        public String tagSelect
        {
            get
            {
                return tagSelectVal;
            }
            set
            {
                if (this.tagSelectVal != value)
                {
                    this.tagSelectVal = value;
                }
                NotifyPropertyChanged();
            }
        }
        public ModController Model
        {

            get { return modP; }

            set
            {
                if (this.modP != value)
                {
                    this.modP = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool tagCanChange(String g)
        {

            string a = modP.tag;
            string b = this.tagSelect;
            return a != b;
        }

        internal void changeVisibilityButtonAndStatus(bool v)
        {

            if (v)
            {
                this.buttonUpdateVisible = System.Windows.Visibility.Visible;

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
                this.buttonUpdateVisible = System.Windows.Visibility.Hidden;
            }


        }


        public ModView(ModController mod)
        {
            this.modP = mod;
            this.tags = new ObservableCollection<String>();
            //this.ModController.View = this;
            //this.st = st;
            changeVisibilityButtonAndStatus(false);



        }


        public void InitializeModView()
        {
            if (modP != null && modP.isInstalled())
            {
                this.lastag = modP.lastag;
                this.tagSelect = modP.tag;


                if (modP.GetTags().Count > 0)
                {
                    if (this.tagSelect != this.modP.GetTags().First())
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
                    this.buttonViewMod = "Installer le mod";
                    break;
                case ButtonAction.rien:
                    this.buttonViewMod = "";
                    break;
                case ButtonAction.maj:
                    this.buttonViewMod = "Changer de version";
                    break;
            }



        }
        //public ModController ModController { set;  get; }

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

        public ObservableCollection<String> tags { get; set; }

        private string lastagValue;
        public string lastag
        {
            get
            {
                return lastagValue;
            }

            set
            {

                lastagValue = value;
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
        public async Task updateBranchToTagClickAsync()
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
                    
                    this.tagSelect = modP.tag;
                    this.lastag = modP.lastag;
                    st.labelInfoV = "";
                    st.setTextUpdateAviable(InfoLabelModInstall());
                    st.ScrollLabelInfo();
                    changeVisibilityButtonAndStatus(false);
                }


            }
        }

        public string InfoLabelModInstall()
        {
            return this.repoName + " " + this.modP.tag + " " + "Installé";



        }

        public string InfoLabelModCanUpdate()
        {
            string a = "...Mise à jour de " + this.modP.depot + " " + this.modP.tag + " vers " + this.lastag + " disponible...";
            return a;
        }

    }
}

