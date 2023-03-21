using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModLoader.Controller;
using ModloaderClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModLoader.Utils
{
    public class ServicesModloader
    {
        static public IServiceProvider Service { get; private set; }

        static ServicesModloader() {
            IServiceCollection serviceDescriptors = new ServiceCollection();
            serviceDescriptors.AddDbContext<DBConfigurationContext>(a=>a.UseSqlite("Data Source = "+getSqlitePath()));


            //.Database.Migrate();


            serviceDescriptors.AddDbContext<DBConfigurationContextSqliteCiv>(a => a.UseSqlite("Data Source = " + getSqlitePathModCiv()));
            serviceDescriptors.AddTransient(a => a.GetRequiredService<DBConfigurationContext>().config);
            serviceDescriptors.AddTransient(a => a.GetRequiredService<DBConfigurationContext>().mod);
            serviceDescriptors.AddTransient(a => a.GetRequiredService<DBConfigurationContext>().gitHub);
            serviceDescriptors.AddTransient(a => a.GetRequiredService<DBConfigurationContext>().modSqlite);
            serviceDescriptors.AddTransient(a => a.GetRequiredService<DBConfigurationContext>().modList);



            /**Civ Sqlite**/
            serviceDescriptors.AddTransient(a => a.GetRequiredService<DBConfigurationContextSqliteCiv>().Mods);
            serviceDescriptors.AddTransient(a => a.GetRequiredService<DBConfigurationContextSqliteCiv>().ScannedFiles);
            serviceDescriptors.AddTransient(a => a.GetRequiredService<DBConfigurationContextSqliteCiv>().ModProperties);

            serviceDescriptors.AddTransient<LocalizationModloader>();





            Service = serviceDescriptors.BuildServiceProvider();

        }
        static string getSqlitePath()
        {   

            
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Modloader");
            if(!Directory.Exists(path)) { 
                Directory.CreateDirectory(path);
            }

            string chainesqlite = Path.Combine(path, "modloaderDBConfig.sqlite");
                
            return chainesqlite;


        }


        static string getSqlitePathModCiv()
        {


            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Firaxis Games", "Sid Meier's Civilization VI");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string chainesqlite = Path.Combine(path, "Mods.sqlite");

            return chainesqlite;


        }



    }
}
