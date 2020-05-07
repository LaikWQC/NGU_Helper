using NGU_Helper.Dao;
using NGU_Helper.Model;
using NGU_Helper.Scenarios.Inventory;
using NGU_Helper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGU_Helper.Repo
{
    public class InventoryRepo
    {
        private readonly ZonesDao _zonesDao;
        private readonly ItemsDao _itemsDao;

        public InventoryRepo()
        {
            _zonesDao = new ZonesDao();
            _itemsDao = new ItemsDao();
        }

        public List<Zone_inventory> GetZones()
        {
            var zones = _zonesDao.GetAllZones().OrderByDescending(x=>x.Order);
            return zones.Select(x => Convert(x)).ToList();
        }

        #region Convert
        private Zone_inventory Convert(Zone zone)
        {
            var model = new Zone_inventory()
            {
                Name = zone.Name,
                Order = zone.Order
            };
            model.Items = zone.Items.Select(x => Convert(x)).ToList();
            return model;
        }

        private Item_inventory Convert(Item item)
        {
            var model = new Item_inventory()
            {
                Name = item.Name,
                Level = item.Level,
                Type = item.ItemType,
                Id = item.Id,
                Image = ImageCreator.Create(item.ImageUrl)
            };
            model.Stats = item.Stats.Select(x => Convert(x, model.Level)).ToList();
            return model;
        }

        private Stat_inventory Convert(Stat stat, int level)
        {
            var model = new Stat_inventory(stat.Value)
            {
                Type = stat.StatType,
                Level = level
            };
            return model;
        }
        #endregion
    }
}
