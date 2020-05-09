using NGU_Helper.Model;
using System;

namespace NGU_Helper.Dao
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
                if (stat == null) return;
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
