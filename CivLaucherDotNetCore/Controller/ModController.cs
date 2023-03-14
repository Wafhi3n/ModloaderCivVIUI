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
            if (isInstalled() && m.lastag != null && m.lastag != GitController.GetModTag(this) )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string TagActuel()
        {
            return m.tag;
        }

        public List<String> tags { get; set; }

        public Mod m { get; set; }
        public ModController(Mod m)
        {
            this.m = m;
            if (isInstalled())
            {
                tags = new List<String>();
            }
            else
            {
            }

        }

        public void getTagsFromRepo()
        {
            GitHubApi.getTagsFromRepo(this);
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
                string tag = GitController.GetLastTag(this);
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
            m.lastag = tag;
            DBmod.Update(m);

            services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();


        }
    
        
    }
}
