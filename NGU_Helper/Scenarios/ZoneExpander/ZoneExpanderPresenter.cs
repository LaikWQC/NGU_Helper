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
            foreach(var item in model.Items)
            {
                item.HighlightChanged += OnHighlightChanged;
            }

            _viewmodel = new ZoneExpanderViewModel(model)
            {
                EquipCommand = new DelegateCommand((param) => EquipAction(param))
            };

            _view = new ZoneExpanderView() { DataContext = _viewmodel };
        }

        public event EventHandler<ItemModel> EquipChanged;
        public event EventHandler<bool> HighlightChanged;

        private void EquipAction(object param)
        {
            EquipChanged?.Invoke(this, (ItemModel)param);
        }

        private void OnHighlightChanged(object sender, bool e)
        {
            HighlightChanged?.Invoke(sender, e);
        }
    }
}
