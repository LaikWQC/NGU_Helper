using NGU_Helper.Data;
using System.Collections.Generic;
using System.Linq;

namespace NGU_Helper.Scenarios.Calculating.Criterias
{
    public class ResultList : List<CriteriaResult>
    {
        public ResultList(CriteriaList list, List<StatModel> stats)
        {
            Stats = stats;
            AddRange(list.OrderBy(x=>x.Order).Select(x => new CriteriaResult(x, this)));
            Calculate();
        }
        public ResultList(CriteriaList list)
        {
            AddRange(list.OrderBy(x => x.Order).Select(x => new CriteriaResult(x, this)));
        }

        private List<StatModel> _stats;
        public List<StatModel> Stats 
        {
            get => _stats;
            set
            {
                _stats = value;
                ForEach(x => x.NullValue());
                Calculate();
            }
        }

        private void Calculate()
        {
            foreach(var result in this)
            {
                if (result.IsActive)
                    result.Calculate();
            }
        }
    }
}
