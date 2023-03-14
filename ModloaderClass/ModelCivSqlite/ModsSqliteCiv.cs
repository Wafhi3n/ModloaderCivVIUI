using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModloaderClass.ModelCivSqlite
{
    public class ModsSqliteCiv
    {
        [Key]
        public int? ModRowId { get; set; }
        public int? ScannedFileRowId { get; set; }
        public string? ModId { get; set; }
        public string? Version { get; set; }
               
    }
    public class ScannedFilesCiv
    {
        [Key]
        public int? ScannedFileRowId { get; set; }
        public string? Path { get; set; }
        public int? LastWriteTime { get; set; }

    }
}
