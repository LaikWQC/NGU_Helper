using NGU_Helper.Model;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace NGU_Helper.Dao
{
    public class InventoryDao
    {
        public List<EquipedInventoryItem> GetInventory()
        {
            using (var context = new DataContext())
            {
                var list = context.Inventory.Include(x => x.Item.Stats).ToList();
                return list;
            }
        }

        public void SaveInventory(List<EquipedInventoryItem> itemList)
        {
            using (var context = new DataContext())
            {
                context.Inventory.RemoveRange(context.Inventory);
                context.Inventory.AddRange(itemList);
                context.SaveChanges();
            }
        }
    }
}
