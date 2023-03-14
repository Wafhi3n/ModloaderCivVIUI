using CivLaucherDotNetCore.Controleur;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModLoader.Model;
using ModLoader.Utils;
using ModloaderClass;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace MyApp // Note: actual namespace depends on the project name.
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

            IServiceScope services = Services.Service.CreateScope();
            DbSet<Mod> mod = services.ServiceProvider.GetRequiredService<ModLoader.Model.DBConfigurationContext>().mod;
            //BankMod bm = new BankMod();
            //bm.mods = mod.ToList();
            BankModController bmc = new BankModController();
          // bmc.InstallAll();

           
           bmc.UpdateAllTags();

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

        }
    }
}