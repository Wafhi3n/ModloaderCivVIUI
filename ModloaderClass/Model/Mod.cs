﻿using System;
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

namespace ModloaderClass.Model
{

    public class ModGit 
    {
        [Key]
        public int? Id { get; set; }
        public string? path { get; set; }
        public string? modID { get; set; }
        public string? owner { get; set; }
        public string? depot { get; set; }
        public string? tag { get; set; }
        public string? lastag { get; set; }
        public int? ModRowId { get; set; }
    }

    public class Mod
    {
        [Key]
        public int? Id { get; set; }
        public string? path { get; set; }
        public string? name { get; set; }
        public string? modID { get; set; }
        public bool? isSteam { get; set; }
        public int? ModRowId { get; set; }
        public int? ScannedFileRowId { get; set; }


    }
}

