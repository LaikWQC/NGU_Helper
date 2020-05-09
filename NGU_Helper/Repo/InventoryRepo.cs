using NGU_Helper.Dao;
using NGU_Helper.Data;
using NGU_Helper.Model;
using NGU_Helper.Scenarios.Inventory;
using System;
using System.Collections.Generic;

namespace NGU_Helper.Repo
{
    public class InventoryRepo
    {
        private readonly InventoryDao _dao;
        public InventoryRepo()
        {
            _dao = new InventoryDao();
        }

        public List<EquipedInventoryItem> GetInventory()
        {
            return _dao.GetInventory();
        }

        public void SaveInventory(List<InventoryItem> outfit, List<InventoryItem> accessories)
        {
            var list = new List<EquipedInventoryItem>();
            foreach(var item in outfit)
            {
                if(item.Item!=null)
                {
                    list.Add(Convert(item.Item, null));
                }
            }
            foreach (var item in accessories)
            {
                if (item.Item != null)
                {
                    list.Add(Convert(item.Item, accessories.IndexOf(item)));
                }
            }
            _dao.SaveInventory(list); 
        }

        private EquipedInventoryItem Convert(ItemModel model, int? slot)
        {
            return new EquipedInventoryItem()
            {
                Id = Guid.NewGuid(),
                ItemId = model.Id,
                Slot = slot
            };
        }
    }
}
