using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModLoader.Utils;
using ModloaderClass;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CivLaucherDotNetCore.Controleur
{
    public class ModController
    {
        public Boolean IsUpdateAviable()
        {
            if (isInstalled() && m.lastag != null && m.lastag != GitController.GetModTag(m) )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string TagActuel()
        {
            return m.tag;
        }

        public List<String> tags { get; set; }

        private Mod m { get; set; }
        public ModController(Mod m)
        {
            this.m = m;
            if (isInstalled())
            {
                tags = new List<String>();
                //initLocalRepositoryFromExistingFolder();
                //getReleaseTagsFromApi();
            }
            else
            {
            }

        }

        public Boolean isInstalled()
        {

            if(Directory.Exists(m.path))
            {
                try
                {
                    //Repository rp = new Repository(m.path);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return false;
        }




        

       



    }
}
