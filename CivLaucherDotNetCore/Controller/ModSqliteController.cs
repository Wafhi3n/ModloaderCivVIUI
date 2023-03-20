using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModLoader.Utils;
using ModLoader.Vue.ModsViewers.Model;
using ModloaderClass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModLoader.Controller
{
    public class ModSqliteController
    {

        public ModView vue { get; set; }
        private ModSqlite m { get; set; }

        public ModGitController mGit { get; set; }

        public string modId
        {
            get
            {
                return m.modID;
            }
            set
            {
                m.modID = value;
                //vue.tagSelect = m.tag;
            }

        }

        public int? Id
        {
            get
            {
                return m.Id;
            }
            set
            {
                m.Id = value;
                //vue.tagSelect = m.tag;
            }

        }
        public int? ModRowId
        {
            get
            {
                return m.ModRowId;
            }
            set
            {
                m.ModRowId = value;
                //vue.tagSelect = m.tag;
            }
        }
        public string Path
        {
            get
            {
                return (string)m.path;
            }
            set
            {
                m.path = value;
                //vue.tagSelect = m.tag;
            }
        }
        public Boolean isSteam
        {
            get
            {
                return (Boolean)m.isSteam;
            }
            set
            {
                m.isSteam = value;
                //vue.tagSelect = m.tag;
            }
        }
        public string? name
        {
            get
            {
                return m.name;
            }
            set
            {
                m.name = value;
                //vue.tagSelect = m.tag;
            }
        }
        public int? ScannedFileRowId
        {
            get
            {
                return m.ScannedFileRowId;
            }
            set
            {
                m.ScannedFileRowId = value;
                //vue.tagSelect = m.tag;
            }
        }

        public ModSqliteController()
        {
            vue = new ModView(this);
        }

        public ModSqliteController(ModSqlite mod, Boolean git)
        {
            vue = new ModView(this);
            m = mod;

            if (!(Boolean)mod.isSteam && git)
            {

                IServiceScope services = ServicesModloader.Service.CreateScope();
                DbSet<ModGit> ModGit = services.ServiceProvider.GetRequiredService<DBConfigurationContext>().mod;
                //DbSet<ModSqlite> modsql = services.ServiceProvider.GetRequiredService<DBConfigurationContext>().modSqlite;

                ModGit m = ModGit.ToList().FirstOrDefault(a => a.path == mod.path);
                    
               if (m != null )
                {
                    mGit= new ModGitController(m);
                    mGit.vue = vue;
                    
                    vue.modP = mGit;

                }








                //mGit = new ModGitController();
            }


        }

    }
    }
