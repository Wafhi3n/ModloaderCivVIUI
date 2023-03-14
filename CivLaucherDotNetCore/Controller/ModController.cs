using CivLaucherDotNetCore.Vue.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModLoader.Model;
using ModLoader.Utils;
using ModloaderClass;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CivLaucherDotNetCore.Controleur
{
    public class ModController
    {

        public Boolean IsUpdateAviable()
        {
            if (isInstalled() && m.lastag != null && m.lastag != m.tag )
            {
                //vue.st.setTextUpdateAviable(vue.InfoLabelModCanUpdate());
                return true;
            }
            else
            {
                return false;
            }
        }
        public ModView vue { get; set; }


        private List<String> tags { get; set; }

        public List<String> GetTags()
        {
            return tags;
        }
        public void AddTags(string s)
        {


                tags.Add(s);
            //vue.tags = new ObservableCollection<String>();
                vue.tags.Add(s); 
            
        }


        private Mod m { get; set; }

        public string tag
        {
            get
            {
                return m.tag;
            }
            set { 
                
                m.tag = value; 
                //vue.tagSelect = m.tag;
            }

        }
        public string path
        {
            get
            {
                return m.path;
            }
            set
            {
                m.path = value;
                //vue.tagSelect = m.tag;
            }

        }
        public string depot
        {
            get
            {
                return m.depot;
            }
            set
            {
                m.depot = value;
                //vue.tagSelect = m.tag;
            }

        }
        public string modId
        {
            get
            {
                return m.modID;
            }
            set
            {
                m.modID = value;
                //vue.tagSelect = m.tag;
            }

        }
        public string lastag
        {
            get
            {
                return m.lastag;
            }
            set
            {
                m.lastag = value;
                vue.lastag = m.lastag;
            }

        }


        


        public ModController(Mod m)
        {
            this.m = m;
            if (isInstalled())
            {
                tags = new List<String>();
                GitController.GetModTag(this);

            }
            else
            {
            }
            vue = new ModView(this);
            


        }

        public void getTagsFromRepo()
        {
            GitHubApi.GetTagsFromRepo(this);
            Console.WriteLine(this.tags);
        }
        public Boolean isInstalled()
        {

            if(Directory.Exists(m.path))
            {
                try
                {
                    //Repository rp = new Repository(m.path);
                    GitController.isInstalled(this);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;
        }

        public void Install()
        {
            if (isInstalled())
            {
                //Console.WriteLine(this.m.depot);
                
                //GitController.InstallWithGit(this);
                 GitController.GetLastTag(this);
                //Console.WriteLine(tag);

                //GitController.UpdateToTag(m,tag);
            }
        }

        public string UriRepo()
        {
             IServiceScope services = Services.Service.CreateScope();
            DbSet<Config> Config = services.ServiceProvider.GetRequiredService<ModLoader.Model.DBConfigurationContext>().config;
            string repoUrl = Config.Find("repoUrl").Value;
            return repoUrl + "/" + m.owner + "/" + m.depot + ".git"; 
        }

        public string apiUrl()
        {
            IServiceScope services = Services.Service.CreateScope();
            DbSet<Config> Config = services.ServiceProvider.GetRequiredService<ModLoader.Model.DBConfigurationContext>().config;
            string apiUrl = Config.Find("apiUrl").Value;
            return apiUrl + "/" + m.owner + "/" + m.depot;
        }



        internal void UpdatelastTag(string tag)
        {

            IServiceScope services = Services.Service.CreateScope();
            DbSet<Mod> DBmod = services.ServiceProvider.GetRequiredService<ModLoader.Model.DBConfigurationContext>().mod;
            lastag = tag;
            DBmod.Update(m);

            services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();


        }
    
        
    }
}
