using NGU_Helper.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace NGU_Helper.Scenarios.Inventory
{
    public class Item_inventory
    {
        public Guid Id { get; set; }
        public ItemType Type { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public List<Stat_inventory> Stats { get; set; }
        public BitmapImage Image { get; set; }
        //tooltip
    }
}
