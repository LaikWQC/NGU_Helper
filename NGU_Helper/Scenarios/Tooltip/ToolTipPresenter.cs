using NGU_Helper.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NGU_Helper.Scenarios.Tooltip
{
    class TooltipPresenter : ToolTip
    {
        private readonly TooltipViewModel _viewModel;        

        public TooltipPresenter(ItemModel model)
        {
            Background = Brushes.Transparent;
            BorderThickness = new Thickness(0);

            _viewModel = new TooltipViewModel(model);

            Content = new TooltipView() { DataContext = _viewModel };
        }
    }
}
