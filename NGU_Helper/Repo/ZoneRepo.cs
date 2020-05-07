using NGU_Helper.Dao;
using NGU_Helper.Data;
using NGU_Helper.Model;
using NGU_Helper.Scenarios.ItemList.Models;
using NGU_Helper.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGU_Helper.Repo
{
    public class ZoneRepo
    {
        private readonly ZonesDao _zonesDao;
        private readonly ItemsDao _itemsDao;
        private readonly StatsDao _statsDao;
        public ZoneRepo()
        {
            _zonesDao = new ZonesDao();
            _itemsDao = new ItemsDao();
            _statsDao = new StatsDao();
        }

        public ZoneCollection GetAllZones()
        {
            var zones = _zonesDao.GetAllZones().ToList();
            return new ZoneCollection(zones);
        }
    }
}
