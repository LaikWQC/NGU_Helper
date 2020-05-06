using NGU_Helper.Utils;
using NGU_Helper.Utils.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace NGU_Helper.Scenarios.ItemList.Models
{
    public class Item_ItemList : ViewModelBase
    {        
        public Item_ItemList()
        {
            Stats = new ObservableCollection<Stat_ItemList>();
        }

        public Guid Id { get; set; }
        public Guid ZoneId { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public ItemTypeBl Type { get; set; }
        public ObservableCollection<Stat_ItemList> Stats { get; set; }        
        public string StatsCount => $"({Stats?.Count ?? 0})";
        public string Url { get; set; }
        public BitmapImage Image => ImageCreator.Create(Url);

        public void AddStat(Stat_ItemList stat)
        {
            Stats.Add(stat);
            OnPropertyChanged(nameof(StatsCount));
        }

        public void RemoveStat(Stat_ItemList stat)
        {
            Stats.Remove(stat);
            OnPropertyChanged(nameof(StatsCount));
        }
    }
}
