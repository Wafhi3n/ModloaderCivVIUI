using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ModLoader.Controller;
using ModloaderClass.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModLoader.Utils
{
    public class CheckMods
    {

        Boolean isGameLauched;

        DbSet<Config> dBconfig;

        public CheckMods()
        {
        }

    }
}
