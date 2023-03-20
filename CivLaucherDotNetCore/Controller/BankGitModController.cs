using CivLaucherDotNetCore.Vue;
using CivLauncher;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModLoader.Utils;
using ModLoader.View.ScrollBar;
using ModloaderClass.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ModLoader.Controller
{
    public class BankGitModController
    {
        static readonly HttpClient client = new HttpClient();

        Config config;
        //public BankMod bm { get; set; }
        public List<ModGitController> modsController { get; set; }

        Announcer st { get; set; }
        public BankGitModController(MainFrame mainFrame)
        {
            GetAllModsFromConfig(mainFrame.announcer);

        }
        public void InstallAll()
        {
            foreach (var mod in modsController)
            {
                mod.Install();
            }
        }

        public void UpdateAllTags()
        {
            foreach (ModGitController mod in modsController)
            {
                mod.getTagsFromRepo();
            }
        }




        internal void GetAllModsFromConfig(Announcer st)
        {

            modsController = new List<ModGitController>();
            IServiceScope services = ServicesModloader.Service.CreateScope();
            DbSet<ModGit> DBmod = services.ServiceProvider.GetRequiredService<DBConfigurationContext>().mod;
            //Console.WriteLine(DBmod.Find("1"));
            foreach (ModGit mod in DBmod.ToList())
            {

                /* still installed ?*/

              /*  if (mod.path != null && !Directory.Exists(mod.path))
                {
                    mod.path = null;
                    mod.tag = null;
                }
                if (mod.path == null)
                {

                    mod.tag = null;

                }
                DBmod.Update(mod);*/

                services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();




                ModGitController mc = new ModGitController(mod);



               

                //GitHubApi.GetTagsFromRepo(mc);





                Console.WriteLine(mc.GetTags);
               if(mod.path!= null)
                {
                    GitController.checkClonedMod(mc);
                }

                //mc.vue.modP = mc;
                mc.vue.InitializeModView();
                mc.vue.st = st;

                if (mc.depot !=null && mc.owner !=null)
                {
                    GitHubApi.getLastTagNameReleaseFromRepo(mc);
                    GitHubApi.GetTagsFromRepo(mc);

                }



                modsController.Add(mc);
            }
        }

        public void DisplayisUpdateAviable()
        {
            foreach (ModGitController mc in modsController)
            {
                if (mc.IsUpdateAviable())
                {
                    //mc.View.InfoLabelModCanUpdate();
                }
            }

        }
        public void UpdateAllModLastAviableRelease()
        {
            foreach (ModGitController mod in modsController)
            {
                //  mod.getLastTagNameReleaseFromRepo();
            }
        }

        internal void SetViewScrollText(Announcer st)
        {
            foreach (ModGitController mc in modsController)
            {
                mc.vue.st = st;
            }
        }
        /*public void InitialiseAllModRepoFromPath()
{
   foreach (ModController mod in modsController)
   {
      // mod.initLocalRepositoryFromExistingFolder();
   }
}*/
    }
}
