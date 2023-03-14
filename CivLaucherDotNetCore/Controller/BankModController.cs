using CivLauncher;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModLoader.Utils;
using ModloaderClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CivLaucherDotNetCore.Controleur
{
    public class BankModController
    {
        static readonly HttpClient client = new HttpClient();

        Config config;
        //public BankMod bm { get; set; }
        public List<ModController> modsController { get; set; }
        public BankModController( ScrollText st)
        {
            GetAllModsFromConfig(st);

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
            foreach (ModController mod in modsController)
            {
                mod.getTagsFromRepo();
            }
        }

        


        internal void GetAllModsFromConfig(ScrollText st)
        {

            modsController = new List<ModController>();
            IServiceScope services = Services.Service.CreateScope();
            DbSet<Mod> DBmod = services.ServiceProvider.GetRequiredService<ModLoader.Model.DBConfigurationContext>().mod;
            //Console.WriteLine(DBmod.Find("1"));
            foreach (Mod mod in DBmod.ToList())
            {


                ModController mc = new ModController(mod);
                    
                GitHubApi.GetTagsFromRepo(mc);
                    
                //mc.vue.modP = mc;
                mc.vue.InitializeModView();
                mc.vue.st=st;

                modsController.Add(mc);



            }
        }

        public void DisplayisUpdateAviable()
        {
            foreach (ModController mc in modsController)
            {
                if (mc.IsUpdateAviable())
                {
                    //mc.View.InfoLabelModCanUpdate();
                }
            }
           
        }
        public void UpdateAllModLastAviableRelease()
        {
            foreach (ModController mod in modsController)
            {
             //  mod.getLastTagNameReleaseFromRepo();
            }        
        }

        internal void SetViewScrollText(ScrollText st)
        {
            foreach(ModController mc in modsController)
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
