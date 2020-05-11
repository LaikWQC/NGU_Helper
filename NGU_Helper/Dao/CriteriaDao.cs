using NGU_Helper.Model;
using NGU_Helper.Utils.Enums;
using System.Collections.Generic;
using System.Linq;

namespace NGU_Helper.Dao
{
    public class CriteriaDao
    {
        public void CreateDefault(List<Criteria> list)
        {
            using (var context = new DataContext())
            {
                var criteriaTypes = context.Criteria.Where(x => x.Type == CriteriaType.StatAggregator).Select(x=>x.Type);
                context.Criteria.AddRange(list.SkipWhile(x => criteriaTypes.Contains(x.Type)));
                context.SaveChanges();
            }
        }

        public List<Criteria> GetAll()
        {
            using (var context = new DataContext())
            {
                return context.Criteria.ToList();
            }
        }
    }
}
