using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModloaderClass.Model
{
    public class GitHubCallApiCache
    {

        [Key]

        public string? call { get; set; }
        public string? value { get; set; }
        public string? date { get; set; }
    }
}
