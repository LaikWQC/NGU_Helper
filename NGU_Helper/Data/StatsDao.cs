using NGU_Helper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGU_Helper.Data
{
    public class StatsDao
    {
        public void CreateStat(Stat stat)
        {
            using (var context = new DataContext())
            {
                context.Stats.Add(stat);
                context.SaveChanges();
            }
        }

        public void EditStat(Stat model)
        {
            using (var context = new DataContext())
            {
                var stat = context.Stats.Find(model.Id);
                stat.Value = model.Value;
                stat.StatType = model.StatType;
                context.SaveChanges();
            }
        }

        public void DeleteStat(Guid id)
        {
            using (var context = new DataContext())
            {
                var stat = context.Stats.Find(id);
                context.Stats.Remove(stat);
                context.SaveChanges();
            }
        }
    }
}
