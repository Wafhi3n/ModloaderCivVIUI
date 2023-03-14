using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModloaderClass;
using ModloaderClass.Model;
using ModloaderClass.ModelCivSqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModLoader.Model
{
    public class DBConfigurationContext : DbContext
    {
        public DbSet<Config> config { get; set; }

        public DbSet<Mod> mod { get; set; }
        public DbSet<ModSqlite> modSqlite { get; set; }

        //public DbSet<JsonApiGitReturnLastRelease> jsonApiGitReturnLastRelease { get; set; }

        public DbSet<GitHubCallApiCache> gitHub { get; set; }

        public DBConfigurationContext(DbContextOptions<DBConfigurationContext> dBConfigurationContext) : base (dBConfigurationContext)
        {

        }

    }

    public class DBConfigurationContextSqliteCiv : DbContext
    {

        public DbSet<ModsSqliteCiv> Mods { get; set; }
        public DbSet<ScannedFilesCiv> ScannedFiles { get; set; }

        public DBConfigurationContextSqliteCiv(DbContextOptions<DBConfigurationContextSqliteCiv> dBConfigurationContext) : base(dBConfigurationContext)
        {

        }

    }
}
