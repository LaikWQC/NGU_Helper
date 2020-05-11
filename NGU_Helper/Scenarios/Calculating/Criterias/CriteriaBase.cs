using NGU_Helper.Data;
using NGU_Helper.Model;
using System.Collections.Generic;

namespace NGU_Helper.Scenarios.Calculating.Criterias
{
    public abstract class CriteriaBase
    {
        public CriteriaBase(Criteria criteria)
        {
            Order = criteria.Order;
            IsActive = criteria.IsActive;
            IsActiveInTooltip = criteria.IsActiveForTooltip;
            Name = criteria.Name;
        }

        public int Order { get; set; }
        public bool IsActive { get; set; }
        public bool IsActiveInTooltip { get; set; }
        public string Name { get; set; }

        public abstract void Calculate(ResultList result, List<StatModel> stats);
    }
}
