using System;
using System.Collections.Generic;
using System.Configuration;

namespace ModloaderClass
{
    public class BankMod
    {
        public List<Mod>? mods { get; set; }
        public BankMod()
        {
            mods = new List<Mod>();
        }


    }
}