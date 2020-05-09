using NGU_Helper.Utils;
using NGU_Helper.Utils.Enums;
using System;
using System.Windows.Media.Imaging;
using NGU_Helper.Model;
using System.Windows.Controls;
using NGU_Helper.Scenarios.Tooltip;
using System.Windows.Input;

namespace NGU_Helper.Data
{
    public class ItemModel : ViewModelBase
    {
        public ItemModel(Guid zoneId)
        {
            ZoneId = zoneId;
            Stats = new StatCollection();
        }

        public ItemModel(Item item)
        {
            Id = item.Id;
            ZoneId = item.ZoneId;
            Name = item.Name;
            Number = item.Number;
            Level = item.Level;
            Type = new ItemTypeBl(item.ItemType);
            Stats = new StatCollection(item.Stats, item.Level);
            Url = item.ImageUrl;
        }

        public Guid Id { get; set; }
        public Guid ZoneId { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public int Level { get; set; }
        public ItemTypeBl Type { get; set; }
        public StatCollection Stats { get; set; }
        public string StatsCount => $"({Stats?.Count ?? 0})";
        public string Url { get; set; }
        public BitmapImage Image => ImageCreator.Create(Url);        

        public void AddStat(StatModel stat)
        {
            Stats.Add(stat);
            OnPropertyChanged(nameof(StatsCount));
        }

        public void RemoveStat(StatModel stat)
        {
            Stats.Remove(stat);
            OnPropertyChanged(nameof(StatsCount));
        }

        #region Tooltip
        private bool _needTooltip;
        private TooltipPresenter _tooltipPresenter;

        private ToolTip _tooltip;
        public ToolTip Tooltip => _tooltip ?? (_tooltip = new ToolTip());

        public ICommand _mouseEnterCommand;
        public ICommand MouseEnterCommand => _mouseEnterCommand ?? (new DelegateCommand(UpdateTooltip));

        private void UpdateTooltip()
        {
            //изначально у нас стоит заглушка на тултип
            //при наведении мышкой создаем презентер и заменяем им нашу заглушку
            if(_tooltipPresenter == null)
            {
                _tooltipPresenter = new TooltipPresenter(this);
                _tooltip = _tooltipPresenter;
                OnPropertyChanged(nameof(ToolTip));
            }
            //если данные изменились, нужно пересчитать тултип и обновить
            else
            {
                if (_needTooltip)
                {
                    _needTooltip = false;
                    //_tooltipPresenter.Update();
                    OnPropertyChanged(nameof(ToolTip));
                }
            }                       
        }
        #endregion
    }
}
