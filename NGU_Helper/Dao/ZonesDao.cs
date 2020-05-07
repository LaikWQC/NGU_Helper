using NGU_Helper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace NGU_Helper.Dao
{
    public class ZonesDao
    {
        public List<Zone> GetAllZones()
        {
            using (var context = new DataContext())
            {
                var zones = context.Zones.Include(x => x.Items.Select(i => i.Stats)).ToList();
                return zones;
            }
        }

        public void CreateZone(Zone zone)
        {
            using (var context = new DataContext())
            {                
                context.Zones.Add(zone);
                context.SaveChanges();
            }
        }

        public void EditZone(Zone model)
        {
            using (var context = new DataContext())
            {
                var zone = context.Zones.Find(model.Id);
                if (zone == null) return;
                zone.Name = model.Name;
                zone.Order = model.Order;
                context.SaveChanges();
            }
        }

        public void DeleteZone(Guid id)
        {
            using (var context = new DataContext())
            {
                var zone = context.Zones.Find(id);
                context.Zones.Remove(zone);
                context.SaveChanges();
            }
        }
    }
}
