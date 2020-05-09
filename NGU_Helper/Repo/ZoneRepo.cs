using NGU_Helper.Dao;
using NGU_Helper.Data;
using NGU_Helper.Model;
using System;
using System.Linq;

namespace NGU_Helper.Repo
{
    public class ZoneRepo
    {
        private readonly ZonesDao _dao;
        public ZoneRepo()
        {
            _dao = new ZonesDao();
        }

        public ZoneCollection GetAllZones()
        {
            var zones = _dao.GetAllZones().ToList();
            return new ZoneCollection(zones);
        }

        public void CreateZone(ZoneModel model)
        {
            _dao.CreateZone(Convert(model));
        }

        public void EditZone(ZoneModel model)
        {
            _dao.EditZone(Convert(model));
        }

        public void DeleteZone(Guid id)
        {
            _dao.DeleteZone(id);
        }

        private Zone Convert(ZoneModel model)
        {
            return new Zone()
            {
                Id = model.Id,
                Name = model.Name,
                Order = model.Order
            };
        }
    }
}
