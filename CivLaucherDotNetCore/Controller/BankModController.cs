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
        public BankMod bm { get; set; }
        public List<ModController> modsController { get; set; }
        public BankModController(BankMod bm)
        {
            this.bm = bm;
            IServiceScope services = Services.Service.CreateScope();
            DbSet < Config > config= services.ServiceProvider.GetRequiredService<ModLoader.Model.DBConfigurationContext>().config;
            modsController = new List<ModController>();
        }
        internal void GetAllModsFromConfig()
        {
            foreach (Mod mod in bm.mods)
            {
                Mod m = new Mod();           
                bm.mods.Add(m);
                ModController mc = new ModController(m);
                //m.mController = mc;
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
        public void InitialiseAllModRepoFromPath()
        {
            foreach (ModController mod in modsController)
            {
               // mod.initLocalRepositoryFromExistingFolder();
            }
        }
    }
}
