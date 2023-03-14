using CivLaucherDotNetCore.Controleur;
using CivLauncher;
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
        install = 0 ,
        maj     = 1 ,
        rien    = 2           
    }
    class ModView : INotifyPropertyChanged
    {
        private ModController modP;
        public event PropertyChangedEventHandler PropertyChanged;
        ScrollText st;

        private string buttonViewModVal;
       
        
        System.Windows.Visibility visibility;

        public System.Windows.Visibility buttonUpdateVisible { get
            {
                return visibility;

            }
            set { this.visibility = value;
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
        public ModController Model {

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

            string a = this.ModController.TagActuel();
            string b = this.tagSelect;
            return  a != b;
        }

        internal void changeVisibilityButtonAndStatus(bool v)
        {

            if (v)
            {
                this.buttonUpdateVisible = System.Windows.Visibility.Visible;

                if (!ModController.isInstalled())
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


        public ModView(ModController mod, ScrollText st)
        {
            this.modP = mod;
            //this.ModController.View = this;
            this.st = st;
            //changeVisibilityButtonAndStatus(false);

            if (ModController != null && ModController.isInstalled())
            {
                this.tagSelect = ModController.TagActuel();
                if (ModController.tags.Count > 0)
                {
                    this.derniereVersionDisponible = ModController.tags[0];
                    if (this.tagSelect != this.ModController.tags.First())
                    {
                        changeButtonMod(ButtonAction.maj);

                    }
                    else
                    {
                        changeButtonMod(ButtonAction.rien);

                    }
                }
                if (this.ModController.IsUpdateAviable())
                {
                    st.setTextUpdateAviable(InfoLabelModCanUpdate());
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
        public ModController ModController { set;  get; }

        public string repoName
        {
            get
            {
                return modP.m.depot;
            }

            set
            {
                modP.m.depot = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<String> tags { get; set; }
   
        private string derniereVersionDisponibleValue;
        public string derniereVersionDisponible
        {
            get
            {
                return derniereVersionDisponibleValue;
            }

            set
            {

                derniereVersionDisponibleValue = value;
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
                //await ModController.updateBranchToTagAsync(tagSelect);
                st.labelInfoV = "";
                st.setTextUpdateAviable(InfoLabelModInstall());
                st.ScrollLabelInfo();
                changeVisibilityButtonAndStatus(false);
            }
            else
            {
                if (!this.ModController.isInstalled())
                {
                    //await ModController.cloneMod();
                    this.tagSelect = ModController.TagActuel();
                    //this.derniereVersionDisponible = this.ModController.LastTag.FriendlyName;
                    st.labelInfoV = "";
                    st.setTextUpdateAviable(InfoLabelModInstall());
                    st.ScrollLabelInfo();
                    changeVisibilityButtonAndStatus(false);
                }


            }
        }

        public string InfoLabelModInstall()
        {
           return this.repoName + " " + this.ModController.TagActuel() + " " + "Installé";

           

        }

        public string  InfoLabelModCanUpdate()
        {
            string a = "...Mise à jour de " + this.repoName +" "+ this.ModController.TagActuel() + " vers " + this.tags.First() + " disponible...";
            return a;    
        }

    }
}

