using NGU_Helper.Utils;
using NGU_Helper.Utils.Enums;
using System;
using System.Windows.Media.Imaging;
using NGU_Helper.Model;

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
        public virtual int Level { get; set; }
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
    }
}
