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
        }

        public ObservableCollection<Zone_ItemList> Zones { get; set; }
        public ObservableCollection<Item_ItemList> Items => SelectedZone?.Items;
        public ObservableCollection<Stat_ItemList> Stats => SelectedItem?.Stats;

        private Zone_ItemList _selectedZone;
        public Zone_ItemList SelectedZone
        {
            get => _selectedZone;
            set
            {
                _selectedZone = value;
                OnPropertyChanged(nameof(Items));
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
                OnPropertyChanged(nameof(Stats));
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
