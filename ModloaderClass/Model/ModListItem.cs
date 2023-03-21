using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModloaderClass.Model
{
    public class ModListItem
    {
        public int Id;
        public int ModListId { get; set; }
        public int ModListItemId { get; set; }
        public String modID { get; set; }

    }
}
