using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModLoader.Model;
using ModloaderClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModLoader.Utils
{
    public class Services
    {
        static public IServiceProvider Service { get; private set; }

        static Services() {
            IServiceCollection serviceDescriptors = new ServiceCollection();
            serviceDescriptors.AddDbContext<DBConfigurationContext>(a=>a.UseSqlite("Data Source = "+getSqlitePath()));
            serviceDescriptors.AddDbContext<DBConfigurationContextSqliteCiv>(a => a.UseSqlite("Data Source = " + getSqlitePathModCiv()));
            serviceDescriptors.AddTransient(a => a.GetRequiredService<DBConfigurationContext>().config);
            serviceDescriptors.AddTransient(a => a.GetRequiredService<DBConfigurationContext>().mod);
            serviceDescriptors.AddTransient(a => a.GetRequiredService<DBConfigurationContext>().gitHub);



                    /**Civ Sqlite**/
            serviceDescriptors.AddTransient(a => a.GetRequiredService<DBConfigurationContextSqliteCiv>().Mods);
            serviceDescriptors.AddTransient(a => a.GetRequiredService<DBConfigurationContextSqliteCiv>().ScannedFiles);



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


            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "My Games", "Sid Meier's Civilization VI");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string chainesqlite = Path.Combine(path, "Mods.sqlite");

            return chainesqlite;


        }



    }
}
