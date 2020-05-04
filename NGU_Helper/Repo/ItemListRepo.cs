using NGU_Helper.Data;
using NGU_Helper.Model;
using NGU_Helper.Scenarios.ItemList.Models;
using NGU_Helper.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGU_Helper.Repo
{
    public class ItemListRepo
    {
        private readonly ZonesDao _zonesDao;
        private readonly ItemsDao _itemsDao;
        private readonly StatsDao _statsDao;
        public ItemListRepo()
        {
            _zonesDao = new ZonesDao();
            _itemsDao = new ItemsDao();
            _statsDao = new StatsDao();
        }

        #region Zone
        public List<Zone_ItemList> GetAllZones()
        {
            return _zonesDao.GetAllZones().Select(x=>Convert(x)).ToList();
        }

        public Guid CreateZone(Zone_ItemList model)
        {
            var zone = Convert(model);
            var id = Guid.NewGuid();
            zone.Id = id;
            _zonesDao.CreateZone(zone);
            return id;
        }

        public void EditZone(Zone_ItemList model)
        {
            _zonesDao.EditZone(Convert(model));
        }

        public void DeleteZone(Guid id)
        {
            _zonesDao.DeleteZone(id);
        }
        #endregion

        #region Item
        public Guid CreateItem(Item_ItemList model)
        {
            var item = Convert(model);
            var id = Guid.NewGuid();
            item.Id = id;
            _itemsDao.CreateItem(item);
            return id;
        }

        public void EditItem(Item_ItemList model)
        {
            _itemsDao.EditItem(Convert(model));
        }

        public void DeleteItem(Guid id)
        {
            _itemsDao.DeleteItem(id);
        }
        #endregion

        #region Stat
        public Guid CreateStat(Stat_ItemList model)
        {
            var stat = Convert(model);
            var id = Guid.NewGuid();
            stat.Id = id;            
            _statsDao.CreateStat(stat);
            return id;
        }

        public void EditStat(Stat_ItemList model)
        {
            _statsDao.EditStat(Convert(model));
        }

        public void DeleteStat(Guid id)
        {
            _statsDao.DeleteStat(id);
        }
        #endregion

        #region Convert
        private Zone_ItemList Convert(Zone zone)
        {
            var model = new Zone_ItemList()
            {
                Name = zone.Name,
                Order = zone.Order,
                Id = zone.Id
            };
            model.Items = zone.Items.Select(x => Convert(x)).ToList();
            return model;
        }

        private Item_ItemList Convert(Item item)
        {
            var model = new Item_ItemList()
            {
                Name = item.Name,
                Level = item.Level,
                Type = new ItemTypeBl(item.ItemType),
                Url = item.ImageUrl,
                Id = item.Id,
                ZoneId = item.ZoneId
            };
            model.Stats = item.Stats.Select(x => Convert(x)).ToList();
            return model;
        }

        private Stat_ItemList Convert(Stat stat)
        {
            var model = new Stat_ItemList()
            {
                Type = new StatTypeBl(stat.StatType),
                Value = stat.Value,
                Id = stat.Id,
                ItemId = stat.ItemId
            };
            return model;
        }

        private Zone Convert(Zone_ItemList model)
        {
            return new Zone()
            {
                Id = model.Id,
                Name = model.Name,
                Order = model.Order
            };
        }

        private Item Convert(Item_ItemList model)
        {
            return new Item()
            {
                Id = model.Id,
                ZoneId = model.ZoneId,
                Name = model.Name,
                Level = model.Level,
                ItemType = model.Type.Type,
                ImageUrl = model.Url
            };
        }

        private Stat Convert(Stat_ItemList model)
        {
            return new Stat()
            {
                Id = model.Id,
                ItemId = model.ItemId,
                Value = model.Value,
                StatType = model.Type.Type
            };
        }
        #endregion
    }
}
