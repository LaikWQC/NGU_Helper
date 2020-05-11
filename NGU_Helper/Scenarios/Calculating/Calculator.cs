using NGU_Helper.Data;
using NGU_Helper.Repo;
using NGU_Helper.Scenarios.Calculating.Criterias;
using System.Collections.Generic;

namespace NGU_Helper.Scenarios.Calculating
{
    public class Calculator
    {
        private static Calculator _calculator;
        private Inventory.Inventory _invetory;
        private readonly CriteriaRepo _repo;
        private CriteriaList _criterias;

        private Calculator()
        {
            _repo = new CriteriaRepo();
            _criterias = new CriteriaList(_repo.GetAll());
        }

        public static Calculator Get()
        {
            return _calculator ?? (_calculator = new Calculator());
        }

        public void SetInventory(Inventory.Inventory invetory)
        {
            _invetory = invetory;
        }

        /// <summary>
        /// считает статы одетых сейчас вещей
        /// </summary>
        public ResultList Calculate()
        {
            var stats = new List<StatModel>();
            _invetory.GetEquiped().ForEach(x => stats.AddRange(x.Stats));
            return new ResultList(_criterias, stats);
        }

        /// <summary>
        /// считает статы в случае надевания/снятия этой вещи
        /// </summary>
        public ResultList Calculate(ItemModel item)
        {
            throw new System.NotImplementedException();
        }
    }
}
