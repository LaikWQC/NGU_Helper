using NGU_Helper.Model;
using NGU_Helper.Utils.Enums;
using System.Collections.Generic;

namespace NGU_Helper.Scenarios.Calculating.Criterias
{
    public class CriteriaList : List<CriteriaBase>
    {
        public CriteriaList(List<Criteria> list)
        {
            list.ForEach(x => _add(x));
        }
        
        private void _add(Criteria criteria)
        {
            if(criteria.Type == CriteriaType.StatAggregator)
            {
                Add(new StatAggregateCriteria(criteria));
            }
        }
    }
}
