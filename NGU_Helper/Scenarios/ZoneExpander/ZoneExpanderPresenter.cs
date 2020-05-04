using NGU_Helper.Scenarios.Inventory;
using System.Windows.Controls;

namespace NGU_Helper.Scenarios.ZoneExpander
{
    public class ZoneExpanderPresenter
    {
        private readonly ZoneExpanderViewModel _viewmodel;
        private readonly ZoneExpanderView _view;

        public ZoneExpanderPresenter(Zone_inventory model)
        {
            _viewmodel = new ZoneExpanderViewModel(model);

            _view = new ZoneExpanderView() { DataContext = _viewmodel };
        }

        public ContentControl ViewContent => _view;
    }
}
