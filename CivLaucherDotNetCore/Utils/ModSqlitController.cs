using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModLoader.Model;
using ModloaderClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModLoader.Utils
{
    public abstract class ModSqlitController
    {

         public static void importDataToDB()
        {

            //IServiceScope services = Services.Service.CreateScope();
            //DbSet<Mod> DBmod = services.ServiceProvider.GetRequiredService<ModLoader.Model.DBConfigurationContext>().mod;
            //GitHubCallApiCache gc = DBapi.ToList().Find(a => a.call == call);
            //DBapi.Update(gc);
            //services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();
        }
    }
}
