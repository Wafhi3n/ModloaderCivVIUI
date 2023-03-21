using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModLoader.Utils;
using ModloaderClass.Model;
using ModloaderClass.ModelCivSqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ModLoader.Controller
{
    public class DBConfigurationContext : DbContext
    {
        public DbSet<Config> config { get; set; }
        public DbSet<ModGit> mod { get; set; }
        public DbSet<Mod> modSqlite { get; set; }
        public DbSet<GitHubCallApiCache> gitHub { get; set; }
        public DbSet<ModList> modList { get; set; }
        public DbSet<ModListItem> modListItem { get; set; }

            

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ModList>()
                .HasKey(c => new { c.Id });
            modelBuilder.Entity<ModListItem>()
                .HasKey(c => new { c.Id });
            modelBuilder.Entity<ModListItem>().HasIndex(c => new { c.modID, c.ModListId }).IsUnique();
        }
        public DBConfigurationContext(DbContextOptions<DBConfigurationContext> dBConfigurationContext) : base(dBConfigurationContext)
        {
            Database.Migrate();
        }


        public async Task<T> GetConfiguration<T>(string key, T defaultValue)
        {

            Config? conf = await config.Where(a => a.Key == key).FirstOrDefaultAsync();

            if (conf == null)
            {
                conf = new Config() { Key = key, Value = JsonSerializer.Serialize(defaultValue) };
                Entry(conf).State = EntityState.Added;
                await SaveChangesAsync();
                return defaultValue;
            }

            if (conf.Value == null)
                return defaultValue;
            var data = JsonSerializer.Deserialize<T>(conf.Value);

            if (data == null)
            {
                return defaultValue;
            }

            return data;
        }









        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source = " + getSqlitePath());
            }
        }
        static string getSqlitePath()
        {


            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Modloader");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string chainesqlite = Path.Combine(path, "modloaderDBConfig.sqlite");

            return chainesqlite;


        }




    }







    public class DBConfigurationContextSqliteCiv : DbContext
    {

        public DbSet<ModsSqliteCiv> Mods { get; set; }
        public DbSet<ScannedFilesCiv> ScannedFiles { get; set; }
        public DbSet<ModsPropertiesCiv> ModProperties { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ModsPropertiesCiv>()
                .HasKey(c => new { c.ModRowId, c.Name });
        }



        public DBConfigurationContextSqliteCiv(DbContextOptions<DBConfigurationContextSqliteCiv> dBConfigurationContext) : base(dBConfigurationContext)
        {

        }

    }
}

