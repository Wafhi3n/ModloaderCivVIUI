using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Win32;
using ModLoader.Controller;
using ModloaderClass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModLoader.Utils
{

    public class Shorcut
    {
        string pathX12 { get; set; }

        string path { get; set; }

        public Shorcut()
        {
            IServiceScope services = ServicesModloader.Service.CreateScope();
            DbSet<Config> config = services.ServiceProvider.GetRequiredService<DBConfigurationContext>().config;
            string path = config.First(a => a.Key == "CivPath").Value ;
            string pathX12 = config.First(a => a.Key == "CivPathX12").Value;
            if (path != null)
            {
                this.path = path;
            }
            if (pathX12 != null)
            {
                this.pathX12 = pathX12;
            }
            services.ServiceProvider.GetService<DBConfigurationContext>().Dispose();
        }

        public void GetCivDir()
        {
            IServiceScope services = ServicesModloader.Service.CreateScope();
            DbSet<Config> config = services.ServiceProvider.GetRequiredService<DBConfigurationContext>().config;
            RegistryKey reg = Registry.CurrentUser;
            RegistryKey subKey = reg.OpenSubKey("System\\GameConfigStore\\Children");

            var lookUpKey = subKey.GetSubKeyNames();

            foreach (var a in lookUpKey)
            {
                RegistryKey subkey2 = subKey.OpenSubKey(a);

                string subkey3 = (string)subkey2.GetValue("MatchedExeFullPath");

                // Console.Write(subkey3+"\n");

                if (subkey3 != null && (subkey3.Contains("CivilizationVI.exe")))
                {
                    path = subkey3;
                    Config pathU = config.First(a => a.Key == "CivPath");
                    pathU.Value = subkey3;
                    config.Update(pathU);
                    services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();

                }
                if (subkey3 != null && (subkey3.Contains("CivilizationVI_DX12.exe")))
                {
                    pathX12 = subkey3;
                    Config pathU = config.First(a => a.Key == "CivPathX12");
                    pathU.Value = subkey3;
                    config.Update(pathU);
                    services.ServiceProvider.GetService<DBConfigurationContext>().SaveChanges();

                }
            }
            services.ServiceProvider.GetService<DBConfigurationContext>().Dispose();
        }

        public string GetSteamX12Dir()
        {

            return ("steam://rungameid/289070//"+pathX12+ " %command%");
        }
        

    }
}
