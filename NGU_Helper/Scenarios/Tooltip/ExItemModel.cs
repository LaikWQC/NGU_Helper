using NGU_Helper.Data;
using NGU_Helper.Model;
using NGU_Helper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace NGU_Helper.Scenarios.Tooltip
{
    public class ExItemModel : ItemModel
    {
        private bool _needTooltip;
        private TooltipPresenter _tooltipPresenter;

        public static ExItemModel Convert(ItemModel item)
        {
            return new ExItemModel(item.ZoneId)
            {
                Id = item.Id,
                Name = item.Name,
                Number = item.Number,
                _level = item.Level,
                Type = item.Type,
                Stats = item.Stats,
                Url = item.Url,
            };
        }

        public ExItemModel(Guid zoneId) : base(zoneId) { }

        public ExItemModel(Item item) : base(item) { }

        private int _level;
        public override int Level
        {
            get => _level;
            set
            {
                Level = value;
                OnPropertyChanged(nameof(_level));
            }
        }

        private ToolTip _tooltip;
        public ToolTip Tooltip => _tooltip ?? (_tooltip = new ToolTip());

        private void MouseEnterAction(object param)
        {
            UpdateTooltip();
            if (param == null || param.GetType() == typeof(ItemModel))
            {
                HighlightChanged?.Invoke(this, true);
            }
        }

        private void MouseLeaveAction(object param)
        {
            if (param == null || param.GetType() == typeof(ItemModel))
            {
                HighlightChanged?.Invoke(this, false);
            }
        }

        private void UpdateTooltip()
        {
            //изначально у нас стоит заглушка на тултип
            //при наведении мышкой создаем презентер и заменяем им нашу заглушку
            if (_tooltipPresenter == null)
            {
                _tooltipPresenter = new TooltipPresenter(this);
                _tooltip = _tooltipPresenter;
                OnPropertyChanged(nameof(Tooltip));
            }
            //если данные изменились, нужно пересчитать тултип и обновить
            else
            {
                if (_needTooltip)
                {
                    _needTooltip = false;
                    //_tooltipPresenter.Update();
                    OnPropertyChanged(nameof(Tooltip));
                }
            }
        }

        public event EventHandler<bool> HighlightChanged;

        private ICommand _mouseEnterCommand;
        public ICommand MouseEnterCommand => _mouseEnterCommand ?? (_mouseEnterCommand = new DelegateCommand(MouseEnterAction));

        private ICommand _mouseLeaveCommand;
        public ICommand MouseLeaveCommand => _mouseLeaveCommand ?? (_mouseLeaveCommand = new DelegateCommand(MouseLeaveAction));
    }
}
