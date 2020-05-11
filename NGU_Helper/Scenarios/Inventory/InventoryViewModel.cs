using NGU_Helper.Scenarios.Calculating.Criterias;
using NGU_Helper.Utils;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace NGU_Helper.Scenarios.Inventory
{
    public class InventoryViewModel : ViewModelBase
    {
        private readonly Inventory _model;

        public InventoryViewModel(Inventory model)
        {
            _model = model;
            _model.PropertyChanged += OnModelPropertyChanged;
        }

        private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Inventory.Result))
                OnPropertyChanged(nameof(Criterias));
        }

        public List<InventoryItem> Outfit => _model.Outfit;
        public List<InventoryItem> Accessories => _model.Accessories;
        public ResultList Criterias => _model.Result;

        public ICommand UnequipCommand => new DelegateCommand((param) => _model.UnEquip((InventoryItem)param));
    }
}
