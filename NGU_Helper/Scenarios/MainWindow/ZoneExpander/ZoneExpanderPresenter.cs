using NGU_Helper.Data;
using NGU_Helper.Scenarios.Tooltip;
using NGU_Helper.Utils;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace NGU_Helper.Scenarios.MainWindow.ZoneExpander
{
    public class ZoneExpanderPresenter
    {
        private readonly ZoneExpanderViewModel _viewmodel;
        private readonly ZoneExpanderView _view;

        public ContentControl ViewContent => _view;
        public ZoneExpanderPresenter(ZoneModel model)
        {
            var items = new List<ExItemModel>();
            foreach(var item in model.Items)
            {
                var exItem = ExItemModel.Convert(item);
                exItem.HighlightChanged += OnHighlightChanged;
                items.Add(exItem);
            }

            _viewmodel = new ZoneExpanderViewModel()
            {
                EquipCommand = new DelegateCommand((param) => EquipAction(param)),
                Header = model.Name,
                Items = items
            };

            _view = new ZoneExpanderView() { DataContext = _viewmodel };
        }

        public event EventHandler<ExItemModel> EquipChanged;
        public event EventHandler<bool> HighlightChanged;

        private void EquipAction(object param)
        {
            EquipChanged?.Invoke(this, (ExItemModel)param);
        }

        private void OnHighlightChanged(object sender, bool e)
        {
            HighlightChanged?.Invoke(sender, e);
        }
    }
}
