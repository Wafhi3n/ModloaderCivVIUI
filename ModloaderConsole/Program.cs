using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using ModLoader.Controller;
using ModLoader.Utils;
using ModloaderClass;
using ModloaderClass.Model;
using ModloaderClass.ModelCivSqlite;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows.Markup;
using System.Windows.Media.Animation;

namespace ModloaderConsole // Note: actual namespace depends on the project name.
{
    internal class Program
    {
       static void Main(string[] args)
        {
            //        Console.WriteLine("Hello World!");

            //          Console.WriteLine(System.Reflection.Assembly.GetExecutingAssembly().Location);

            // Directory.SetCurrentDirectory(@"c:\program files\");

            // IServiceScope services = Services.Service.CreateScope();
            //DbSet<Mod> mod = services.ServiceProvider.GetRequiredService<ModLoader.Model.DBConfigurationContext>().mod;

            //Mod m = mod.ToList().Find((x => x.depot == "BetterBalancedGame"));
            //Console.WriteLine(config.First().Id);           //Mod mod = services.ServiceProvider.GetRequiredService<Mod>();


            /* DbSet<ScannedFilesCiv> scannedFile = services.ServiceProvider.GetRequiredService<ModLoader.Model.DBConfigurationContextSqliteCiv>().ScannedFiles;
             DbSet<ModsPropertiesCiv> modProperties = services.ServiceProvider.GetRequiredService<ModLoader.Model.DBConfigurationContextSqliteCiv>().ModProperties;
             DbSet<ModSqlite> modSqlite = services.ServiceProvider.GetRequiredService<ModLoader.Model.DBConfigurationContext>().modSqlite;

             */
            //.FindAll()  a => a.Value != "LOC_MOD_AUTHORS_FIRAXIS" a => a.Name == "Authors"
            //modSqlite.ToList();

            //string testP = "D:/Mathieu/Documents/My Games/Sid Meier's Civilization VI/Mods/MultiplayerHelper/MPH Core v1xx.modinfo";

            //Console.Write(Path.GetDirectoryName(testP));

            /* IServiceScope services = Services.Service.CreateScope();


 services.ServiceProvider.GetService<DBConfigurationContextSqliteCiv>().SaveChanges();*/

            /*  ((System.Data.Entity.Infrastructure.IObjectContextAdapter)dataDb).ObjectContext;
              objCtx.ExecuteStoreCommand("TRUNCATE TABLE [Table]");*/


            /*foreach (ScannedFilesCiv m in scannedFile.ToList().FindAll(a=>a.))
            {
                Console.WriteLine(m);
           }*/

            //string version = SearchKey("HKEY_CURRENT_USER\\System\\GameConfigStore\\Children", "DisplayName", "Google Chrome", "DisplayVersion");

            // RegistryKey uninstallKey = Registry.LocalMachine.OpenSubKey("HKEY_CURRENT_USER\\System\\GameConfigStore\\Children");
            //Registry.CurrentUser.OpenSubKey("\\System\\GameConfigStore\\Children");

            //Console.Write(Registry.CurrentUser.OpenSubKey("\\System\\GameConfigStore\\Children"));


            /* if (string.Equals("CivilizationVI", subkey.GetValue(data, string.Empty).ToString(), StringComparison.CurrentCulture))
             {
                 Console.Write(subkey.GetValue(returnValue).ToString());
             }*/



            //Shorcut shorcut = new Shorcut();
            //shorcut.GetCivDir();



            //BankMod bm = new BankMod();
            //bm.mods = mod.ToList();
            //BankModController bmc = new BankModController();
            //bmc.InstallAll();


            //bmc.UpdateAllTags();

            //Root myDeserializedClass = JsonConvert.DeserializeObject<JsonApiGitReturn>(myJsonResponse);


            /*
            if (!GitController.isInstalled(m))
            {
                Console.WriteLine("Installation");
                //GitController.InstallWithGit(m);

            }
            if (GitController.isInstalled(m))
            {
                GitController.UpdateToTag(m, "5.2.8");
                //GitController.InstallWithGit(m);
            }*/



            //DbSet<Mod> mod = services.ServiceProvider.GetRequiredService<ModLoader.Model.DBConfigurationContext>().mod;

            /*          
                      Mod m = new Mod();
                      m.lastag = "2.9";
                      m.path = "c://hello";
                      m.tag = "2.8";
                      m.depot = "modlaoder";
                      mod.Add(m);
                      Console.WriteLine(config.First().Id);
                      services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();
            */
            //mod.SingleAsync().Wait();


            //IServiceScope services = ServicesModloader.Service.CreateScope();
            //DbSet<ModGit> mod = services.ServiceProvider.GetRequiredService<DBConfigurationContext>().mod;
            //ModGit m = mod.First(a => a.depot == "BetterBalancedGame");
            //ModGitController m2 = new ModGitController(m);


            //m2.Install();


            //Console.WriteLine(m2);

            //GitController.InstallWithGit(m2);



            //GitController.GetLastTag(m2);

            //GitHubApi.getLastTagNameReleaseFromRepo(m2);
            //GitHubApi.GetTagsFromRepo(m2);

            ///GitHubApi.getResultFromCallApiOrDB("_release_latest", "/releases/latest", m2).Wait();





            //GitController.CheckInstallGit();
            // GitController.StartGitCommand("", "/");

            //GitController.InstallGit();




            //GitController.UpdateToTag(m2, "5.2.9");
            //GitController.DeleteWithGit(m2);

            //UpdateToTag

        }
    }
}