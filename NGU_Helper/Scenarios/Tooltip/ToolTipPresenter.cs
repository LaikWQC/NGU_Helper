using NGU_Helper.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NGU_Helper.Scenarios.Tooltip
{
    class TooltipPresenter : ToolTip
    {
        private readonly TooltipViewModel _viewModel;        

        public TooltipPresenter(ItemModel model)
        {
            _viewModel = new TooltipViewModel(model);

            Content = new TooltipView() { DataContext = _viewModel };
        }
    }
}
