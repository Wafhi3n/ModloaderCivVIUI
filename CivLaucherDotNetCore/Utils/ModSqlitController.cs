using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using ModLoader.Controller;
using ModloaderClass.Model;
using ModloaderClass.ModelCivSqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ModLoader.Utils
{
    public abstract class DBModSqlitController
    {

        public static void ImportDataToDB()
        {
            IServiceScope services = ServicesModloader.Service.CreateScope();
            DbSet<Config> conf = services.ServiceProvider.GetRequiredService<DBConfigurationContext>().config;
            if (!conf.ToList().Any())
            {
                services.ServiceProvider.GetService<DBConfigurationContext>().Dispose();
                PopulateConfigTable();
                PopulateModsTable();
            }

            DeleteDataFromDBModSqlite();
            GetModRowIdFromModSqlite();
            GetNameFromModSqlite();
            GetMoIDFromModSqlite();
            GetPathFromModSqlite();
            CheckUninstalledMod();
            AddMissingModsToSqliteMods();

        }
        public static void PopulateConfigTable()
        {
            IServiceScope services = ServicesModloader.Service.CreateScope();
            DbSet<Config> conf = services.ServiceProvider.GetRequiredService<DBConfigurationContext>().config;
            conf.Add(new Config
            {

                Key = "repoUrl",
                Value = "https://github.com/"
            });

            conf.Add(new Config
                {

                    Key = "delayAPI",
                    Value = "15"
                });
            conf.Add(new Config
                {

                    Key = "CivPath",
                    Value = null
                });
            conf.Add(new Config
                {

                    Key = "CivPathX12",
                    Value = null
                });
             conf.Add(new Config
                {
                    Key = "apiUrl",
                    Value = "https://api.github.com/repos"
                });
            services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();
            services.ServiceProvider.GetService<DBConfigurationContext>().Dispose();

        }

        public static void PopulateModsTable()
        {
            IServiceScope services = ServicesModloader.Service.CreateScope();
            DbSet<ModGit> mods = services.ServiceProvider.GetRequiredService<DBConfigurationContext>().mod;

            mods.Add(new ModGit
            {
                modID = "cb84075d-5007-4207-b662-c35a5f7be260",
                owner = "CivilizationVIBetterBalancedGame",
                depot = "BetterBalancedGame"
            });
            mods.Add(new ModGit
            {
                modID = "c88cba8b-8311-4d35-90c3-51a4a5d66550",
                owner = "57fan",
                depot = "Civ6-BBS-2"
            });
            mods.Add(new ModGit
            {
                modID = "c619ac86e-d99d-4bf3-b8f0-8c5b8c402176",
                owner = "CivilizationVIBetterBalancedGame",
                depot = "MultiplayerHelper"
            });
            mods.Add(new ModGit
            {
                modID = "c6e5ad32-0600-4a98-a7cd-5854a1abcaaf",
                owner = "CivilizationVIBetterBalancedGame",
                depot = "BetterSpectatorMod"
            });
            services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();
            services.ServiceProvider.GetService<DBConfigurationContext>().Dispose();
        }
            public static void CheckUninstalledMod()
        {
            IServiceScope services = ServicesModloader.Service.CreateScope();

            DbSet<Mod> modSqlite = services.ServiceProvider.GetRequiredService<DBConfigurationContext>().modSqlite;
            DbSet<ModGit> modGitDB = services.ServiceProvider.GetRequiredService<DBConfigurationContext>().mod;


            foreach (Mod ms in modSqlite.ToList())
            {
                if (!Directory.Exists(ms.path))
                {
                    modSqlite.Remove(ms);
                    
                }
            }
            foreach (ModGit mg in modGitDB.ToList())
            {
                if (!Directory.Exists(mg.path))
                {
                    //modGitDB.Remove(mg);
                    mg.path = null;
                    mg.tag= null;

                }
            }
            services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();
            services.ServiceProvider.GetService<DBConfigurationContext>().Dispose();

        }
        public static void DeleteDataFromDBModSqlite()
        {
            IServiceScope services = ServicesModloader.Service.CreateScope();

            DbSet<Mod> modSqlite = services.ServiceProvider.GetRequiredService<DBConfigurationContext>().modSqlite;

            if (modSqlite != null)
            {
                List<Mod> modSqliteL = modSqlite.ToList();
           
                modSqlite.RemoveRange(modSqlite.ToList());               
    
            }
            services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();
            services.ServiceProvider.GetService<DBConfigurationContext>().Dispose();
        }

        public static void AddMissingModsToSqliteMods()
        {
            IServiceScope services = ServicesModloader.Service.CreateScope();
            DbSet<ModGit> mods = services.ServiceProvider.GetRequiredService<DBConfigurationContext>().mod;
            DbSet<Mod> modSqlite = services.ServiceProvider.GetRequiredService<DBConfigurationContext>().modSqlite;


            foreach (ModGit mg in mods.ToList())
            {
                Console.WriteLine(modSqlite);

                List<Mod> l = modSqlite.ToList().FindAll(a => a.modID == mg.modID);

                List<Mod> link = modSqlite.ToList().FindAll(a => a.modID == mg.modID).FindAll(a =>a.path == mg.path);


                List<ModGit> depot = mods.ToList().FindAll(a => a.depot == mg.depot);









                if (link.Any())
                {          
                    mg.ModRowId = link.First().ModRowId;
                    services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();
                }

                if (!l.Any() && !depot.Any())
                {
                    //bs.AddMod(mg);

                    Mod ms = new Mod();
                    ms.modID = mg.modID;
                    ms.name = mg.depot;
                    ms.isSteam = false;
                    Random rnd = new Random();
                    int random = rnd.Next(2000,3000);
                    ms.ModRowId = random;
                    mg.ModRowId = random;
                    modSqlite.Add(ms);
                    services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();

                }

            }
            services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();
            services.ServiceProvider.GetService<DBConfigurationContext>().Dispose();

        }


        public static void GetModRowIdFromModSqlite()
        {
            IServiceScope services = ServicesModloader.Service.CreateScope();
            DbSet<ModsPropertiesCiv> modProperties = services.ServiceProvider.GetRequiredService<DBConfigurationContextSqliteCiv>().ModProperties;
            DbSet<Mod> modSqlite = services.ServiceProvider.GetRequiredService<DBConfigurationContext>().modSqlite;

            foreach (ModsPropertiesCiv m in (modProperties.ToList().FindAll(a => a.Value != "LOC_MOD_AUTHORS_FIRAXIS").FindAll(b => b.Name == "Authors")))
            {
                Console.WriteLine(m.Value);

                Mod mq = new Mod();
                //mq.name = m.Name;
                mq.ModRowId = m.ModRowId;
                modSqlite.Add(mq);
            }
            services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();
            services.ServiceProvider.GetService<DBConfigurationContext>().Dispose();
            services.ServiceProvider.GetService<DBConfigurationContextSqliteCiv>().Dispose();


        }
        public static void GetNameFromModSqlite()
        {
            IServiceScope services = ServicesModloader.Service.CreateScope();
            DbSet<Mod> modSqlite = services.ServiceProvider.GetRequiredService<DBConfigurationContext>().modSqlite;
            DbSet<ModsPropertiesCiv> modProperties = services.ServiceProvider.GetRequiredService<DBConfigurationContextSqliteCiv>().ModProperties;

            foreach (Mod mq in modSqlite.ToList())
            {
                if (mq.ModRowId !=null) {

                    ModsPropertiesCiv mc = (modProperties.ToList().FindAll(a => a.ModRowId == mq.ModRowId).First(b => b.Name == "Name"));
                    mq.name = mc.Value;
                }
            }
            services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();
            services.ServiceProvider.GetService<DBConfigurationContext>().Dispose();
            services.ServiceProvider.GetService<DBConfigurationContextSqliteCiv>().Dispose();

        }


        public static void GetMoIDFromModSqlite()
        {
            IServiceScope services = ServicesModloader.Service.CreateScope();
            DbSet<Mod> modSqlite = services.ServiceProvider.GetRequiredService<DBConfigurationContext>().modSqlite;

            DbSet<ModsSqliteCiv> modsSqliteciv = services.ServiceProvider.GetRequiredService<DBConfigurationContextSqliteCiv>().Mods;

            foreach (Mod mq in modSqlite.ToList())
            {


                List<ModsSqliteCiv> mc = (modsSqliteciv.ToList().FindAll(a => a.ModRowId == mq.ModRowId)).ToList();
                foreach(ModsSqliteCiv mc2 in mc)
                {
                    mq.modID = mc2.ModId;
                    mq.ScannedFileRowId= mc2.ScannedFileRowId;
                }
                







            }
            services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();
            services.ServiceProvider.GetService<DBConfigurationContext>().Dispose();
        }

        public static void GetPathFromModSqlite()
        {
            IServiceScope services = ServicesModloader.Service.CreateScope();
            DbSet<ModsSqliteCiv> modsSqliteciv = services.ServiceProvider.GetRequiredService<DBConfigurationContextSqliteCiv>().Mods;
            DbSet<ScannedFilesCiv> scannedFile = services.ServiceProvider.GetRequiredService<DBConfigurationContextSqliteCiv>().ScannedFiles;
            DbSet<Mod> modSqlite = services.ServiceProvider.GetRequiredService<DBConfigurationContext>().modSqlite;
            DbSet<ModGit> modgitDB = services.ServiceProvider.GetRequiredService<DBConfigurationContext>().mod;




            foreach (Mod mq in modSqlite.ToList())
            {


                if (mq.ScannedFileRowId != null)
                {
                    ScannedFilesCiv mc = (scannedFile.FirstOrDefault(a => a.ScannedFileRowId == mq.ScannedFileRowId));
                    ModGit mg =modgitDB.ToList().FirstOrDefault(a => a.modID == mq.modID);

                    string path = Path.GetDirectoryName(mc.Path);

                    mq.path = path;






                    mq.isSteam = mc.Path.Contains("/steamapps/workshop/");


                    if (!(Boolean)mq.isSteam && mg != null)
                    {
                        mg.path = path;

                    }

                }






            }
            services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();

            services.ServiceProvider.GetService<DBConfigurationContext>().Dispose();
        }


    }
}
