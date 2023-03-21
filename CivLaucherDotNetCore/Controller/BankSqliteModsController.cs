using CivLaucherDotNetCore.Vue;
using CivLauncher;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModLoader.Utils;
using ModLoader.View.ScrollBar;
using ModLoader.Vue.ModsViewers;
using ModLoader.Vue.ModsViewers.Model;
using ModloaderClass.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModLoader.Controller
{
    public class BankSqliteModsController
    {
        public List<ModSqliteController> mods { get; set; }

        public ObservableCollection<ModView>  OMod { get; set; }

        public List<ModSqliteController> GetMods()
        {
            return mods;
        }

        public void AddMod(ModGit m,Boolean git)
        {

            Mod ms = new Mod();
            ms.modID=m.modID;
            ms.name = m.depot;

            ModSqliteController msc = new ModSqliteController(ms,git);



            mods.Add(msc);
            OMod.Add(msc.vue);
            //gridVue.addModToGrid(msc, this);
            //vue.tags = new ObservableCollection<String>();
            //vue.tags.Add(s);
        }


        public void AddMod(ModSqliteController m)
        {
            

            mods.Add(m);
            //vue.tags = new ObservableCollection<String>();
            //vue.tags.Add(s);
        }

        public  void AddViewToGridView(ModsView modsView)
        {
             

            foreach(ModSqliteController m in mods)
            {
                modsView.addModToGrid(m,this);

            }

        }

        public BankSqliteModsController(MainFrame mainframe)
        {
            //services
            IServiceScope services = ServicesModloader.Service.CreateScope();
            DbSet<Mod> modSqlite = services.ServiceProvider.GetRequiredService<DBConfigurationContext>().modSqlite;
            OMod = new ObservableCollection<ModView>();
            mods = new List<ModSqliteController>(); 
            DBModSqlitController.ImportDataToDB();


            foreach (Mod m in modSqlite.ToList())
            {
                ModSqliteController modSqliteController = new ModSqliteController(m,mainframe.isGitInstalled);
                modSqliteController.vue.announcer = mainframe.announcer;
                AddMod(modSqliteController);
                OMod.Add(modSqliteController.vue);
                
            }

            services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();
            services.ServiceProvider.GetService<DBConfigurationContext>().Dispose();


            //modSqlite.ToList()

        }











    }
    }
