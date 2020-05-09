using NGU_Helper.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGU_Helper.Scenarios.Tooltip
{
    public class TooltipViewModel
    {
        private readonly ItemModel _model;

        public TooltipViewModel(ItemModel model)
        {
            _model = model;
        }

        public ItemModel Model => _model;

        public string Title => $"({_model.Number}) {_model.Name} lvl{_model.Level}";
    }
}
