using NGU_Helper.Scenarios.ItemList.Models;
using NGU_Helper.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NGU_Helper.Scenarios.ItemList
{
    public class ItemListViewModel : ViewModelBase
    {
        public ItemListViewModel()
        {
            Zones = new ObservableCollection<Zone_ItemList>();
            Items = new ObservableCollection<Item_ItemList>();
            Stats = new ObservableCollection<Stat_ItemList>();
        }

        public ObservableCollection<Zone_ItemList> Zones { get; set; }
        public ObservableCollection<Item_ItemList> Items { get; set; }
        public ObservableCollection<Stat_ItemList> Stats { get; set; }

        private Zone_ItemList _selectedZone;
        public Zone_ItemList SelectedZone
        {
            get => _selectedZone;
            set
            {
                _selectedZone = value;
                Items.Clear();
                if (_selectedZone != null && _selectedZone.Items != null) 
                {
                    foreach (var item in _selectedZone.Items)
                    {
                        Items.Add(item);
                    }
                }
                OnPropertyChanged(nameof(SelectedZone));
            }
        }

        private Item_ItemList _selecteditem;
        public Item_ItemList SelectedItem
        {
            get => _selecteditem;
            set
            {
                _selecteditem = value;
                Stats.Clear();
                if (_selecteditem != null && _selecteditem.Stats != null) 
                {
                    foreach (var stat in _selecteditem.Stats)
                    {
                        Stats.Add(stat);
                    }
                }
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        private Stat_ItemList _selectedStat;
        public Stat_ItemList SelectedStat 
        {
            get => _selectedStat;
            set
            {
                _selectedStat = value;
                OnPropertyChanged(nameof(SelectedStat));
            }
        }

        public ICommand AddZoneCommand { get; set; }
        public ICommand EditZoneCommand { get; set; }
        public ICommand DeleteZoneCommand { get; set; }
        public ICommand AddItemCommand { get; set; }
        public ICommand EditItemCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }
        public ICommand AddStatCommand { get; set; }
        public ICommand EditStatCommand { get; set; }
        public ICommand DeleteStatCommand { get; set; }
    }
}
