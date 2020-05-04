using NGU_Helper.Utils;
using NGU_Helper.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGU_Helper.Model
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public ItemType ItemType { get; set; }
        public string ImageUrl { get; set; }
        public Guid ZoneId { get; set; }
        public virtual Zone Zone { get; set; }
        public virtual List<Stat> Stats { get; set; }
    }
}
