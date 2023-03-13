using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModLoader.Model;
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
            //serviceDescriptors.AddTransient(a => a.GetRequiredService<DBConfigurationContext>().configModel.First());
            serviceDescriptors.AddTransient(a => a.GetRequiredService<DBConfigurationContext>().config);
            serviceDescriptors.AddTransient(a => a.GetRequiredService<DBConfigurationContext>().mod);
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
    }
}
