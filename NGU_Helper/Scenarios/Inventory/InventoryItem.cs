using NGU_Helper.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGU_Helper.Scenarios.Inventory
{
    public class InventoryItem
    {
        public InventoryItem(ItemType type)
        {
            Type = type;
        }

        public ItemViewModel Item { get; set; }
        public ItemType Type { get; set; }

        public static List<InventoryItem> GetOufitItems()
        {
            return Enum.GetValues(typeof(ItemType)).Cast<ItemType>()
                .Where(x => x != ItemType.Accessory)
                .Select(x => new InventoryItem(x))
                .ToList();
        }

        public static List<InventoryItem> GetAccessoryItems(int count)
        {
            var list = new List<InventoryItem>();
            for (int i = 0; i < count; i++) 
            {
                list.Add(new InventoryItem(ItemType.Accessory));
            }
            return list;
        }
    }
}
