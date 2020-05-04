using NGU_Helper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace NGU_Helper.Data
{
    public class ZonesDao
    {
        public List<Zone> GetAllZones()
        {
            using (var context = new DataContext())
            {
                var zones = context.Zones.Include(x => x.Items.Select(i => i.Stats)).OrderBy(x => x.Order).ToList();
                foreach (var zone in zones)
                {
                    zone.Items = zone.Items.OrderBy(x => x.ItemType).ToList();
                    zone.Items.ForEach(z => z.Stats = z.Stats.OrderBy(s => s.StatType).ToList());
                }
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

        public void EditZone(Zone zone)
        {
            using (var context = new DataContext())
            {
                var _zone = context.Zones.Find(zone.Id);
                _zone.Name = zone.Name;
                _zone.Order = zone.Order;
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
