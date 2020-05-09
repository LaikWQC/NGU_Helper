using NGU_Helper.Data;
using NGU_Helper.Utils;
using System;
using System.Windows.Controls;

namespace NGU_Helper.Scenarios.ZoneExpander
{
    public class ZoneExpanderPresenter
    {
        private readonly ZoneExpanderViewModel _viewmodel;
        private readonly ZoneExpanderView _view;

        public ContentControl ViewContent => _view;
        public ZoneExpanderPresenter(ZoneModel model)
        {
            _viewmodel = new ZoneExpanderViewModel(model)
            {
                EquipCommand = new DelegateCommand((param) => EquipAction(param))
            };

            _view = new ZoneExpanderView() { DataContext = _viewmodel };
        }

        public event EventHandler<ItemModel> EquipChanged;

        private void EquipAction(object param)
        {
            EquipChanged?.Invoke(this, (ItemModel)param);
        }
    }
}
