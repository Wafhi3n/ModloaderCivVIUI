﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModloaderClass;
using ModloaderClass.Model;
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

        //public DbSet<JsonApiGitReturnLastRelease> jsonApiGitReturnLastRelease { get; set; }
        
        public DbSet<GitHubCallApiCache> gitHub { get; set; }

        public DBConfigurationContext(DbContextOptions<DBConfigurationContext> dBConfigurationContext) : base (dBConfigurationContext)
        {

        }

    }
}
