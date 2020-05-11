using NGU_Helper.Data;
using NGU_Helper.Model;
using NGU_Helper.Utils.Enums;
using System.Collections.Generic;
using System.Linq;

namespace NGU_Helper.Scenarios.Calculating.Criterias
{
    public class StatAggregateCriteria : CriteriaBase
    {
        private readonly StatType _type;
        public StatAggregateCriteria(Criteria criteria) : base(criteria)
        {
            _type = criteria.StatType.Value;
        }

        public override void Calculate(ResultList result, List<StatModel> stats)
        {
            var value = stats.Where(x => x.Type.Type == _type).Sum(x => x.Value);
            result.First(x => x.IsCriteria(this)).Value = value;
        }
    }
}
