using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace ModloaderClass
{
    public class Mod
    {
        [Key]
        public int? Id { get; set; }
        public string? path { get; set; }
        public string? modID { get; set; }
        public string? owner { get; set; }
        public string? depot { get; set; }
        public string? tag { get; set; }
        public string? lastag { get; set; }
    }

    public class ModSqlite
    {
        [Key]
        public int? Id { get; set; }
        public string? path { get; set; }
        public string? modID { get; set; }
        public Boolean isSteam { get; set; }
    }
}

