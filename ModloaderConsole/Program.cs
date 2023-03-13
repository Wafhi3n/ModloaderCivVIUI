using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModLoader.Model;
using ModLoader.Utils;
using ModloaderClass;
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

            IServiceScope services = Services.Service.CreateScope();
            DbSet<Config> config = services.ServiceProvider.GetRequiredService<ModLoader.Model.DBConfigurationContext>().config;
            Console.WriteLine(config.First().Id);           //Mod mod = services.ServiceProvider.GetRequiredService<Mod>();



            DbSet<Mod> mod = services.ServiceProvider.GetRequiredService<ModLoader.Model.DBConfigurationContext>().mod;

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