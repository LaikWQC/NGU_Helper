using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGU_Helper.Scenarios.Inventory
{
    public class Zone_inventory
    {
        public string Name { get; set; }
        public List<Item_inventory> Items { get; set; }
        public int Order { get; set; }
    }
}
