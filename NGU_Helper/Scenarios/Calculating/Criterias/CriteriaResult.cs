namespace NGU_Helper.Scenarios.Calculating.Criterias
{
    public class CriteriaResult
    {
        private readonly CriteriaBase _criteria;
        private readonly ResultList _result;

        public CriteriaResult(CriteriaBase criteria, ResultList result)
        {
            _criteria = criteria;
            _result = result;
        }

        public int Order => _criteria.Order;
        public bool IsActive => _criteria.IsActive;
        public bool IsActiveInTooltip => _criteria.IsActiveInTooltip;
        public string Name => _criteria.Name;

        private double? _value;
        public double Value
        {
            get
            {
                Calculate();
                return _value.Value;
            }
            set
            {
                _value = value;
            }
        }
        public void NullValue()
        {
            _value = null;
        }

        public double FinalValue => 1 + Value / 100;
        public string FinalValueText => $"x{FinalValue}";
        public bool IsCriteria(CriteriaBase criteria)
        {
            return _criteria == criteria;
        }

        public void Calculate()
        {
            if (_value == null)
                _criteria.Calculate(_result, _result.Stats);
        }
    }
}
