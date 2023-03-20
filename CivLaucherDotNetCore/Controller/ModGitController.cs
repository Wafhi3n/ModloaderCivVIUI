using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModLoader.Utils;
using ModLoader.Vue.ModsViewers.Model;
using ModloaderClass.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ModLoader.Controller
{
    public class ModGitController
    {

        public bool IsUpdateAviable()
        {
            if (isInstalled() && m.lastag != null && m.lastag != m.tag)
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


        private List<string> tags { get; set; }

        public List<string> GetTags()
        {
            return tags;
        }
        public void AddTags(string s)
        {


            tags.Add(s);
            vue.tags.Add(s);

        }


        private ModGit m { get; set; }

        public ModGit GetModGit()
        {
            return m;
        }
        public int Id
        {
            get
            {
                return (int)m.Id;
            }
            set
            {

                m.Id = value;
                //vue.tagSelect = m.tag;
            }

        }
        public string tag
        {
            get
            {
                return m.tag;
            }
            set
            {

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

        public string owner
        {
            get
            {
                return m.owner;
            }
            set
            {
                m.owner = value;
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
                //vue.lastag = m.lastag;
            }

        }





        public ModGitController(ModGit m)
        {
            this.m = m;
            tags = new List<string>();

            if (isInstalled())
            {
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
            Console.WriteLine(tags);
        }
        public bool isInstalled()
        {

            if (Directory.Exists(m.path))
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
            if (!isInstalled())
            {

                GitController.InstallWithGit(this);
                GitController.GetLastTag(this);
                GitController.UpdateToTag(this, m.lastag);
                GitController.checkClonedMod(this);
            }
        }

        public string UriRepo()
        {
            IServiceScope services = ServicesModloader.Service.CreateScope();
            DbSet<Config> Config = services.ServiceProvider.GetRequiredService<DBConfigurationContext>().config;
            string repoUrl = Config.Find("repoUrl").Value;
            return repoUrl + "/" + m.owner + "/" + m.depot + ".git";

        }

        public string apiUrl()
        {
            IServiceScope services = ServicesModloader.Service.CreateScope();
            DbSet<Config> Config = services.ServiceProvider.GetRequiredService<DBConfigurationContext>().config;
            string apiUrl = Config.Find("apiUrl").Value;
            return apiUrl + "/" + m.owner + "/" + m.depot;

        }



        internal void UpdatelastTag(string tag)
        {

            IServiceScope services = ServicesModloader.Service.CreateScope();
            DbSet<ModGit> DBmod = services.ServiceProvider.GetRequiredService<DBConfigurationContext>().mod;
            lastag = tag;
            m.lastag = tag;
            DBmod.Update(m);

            services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();
            services.ServiceProvider.GetService<DBConfigurationContext>().Dispose();

        }


    }
}
