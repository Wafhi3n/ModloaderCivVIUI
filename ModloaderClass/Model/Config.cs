using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ModloaderClass
{
    public class Config
    {
        [Key]

        public string? Key { get; set; }
        public string? Value { get; set; }

    }

}
