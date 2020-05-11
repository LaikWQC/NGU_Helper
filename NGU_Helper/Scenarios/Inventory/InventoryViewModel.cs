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
        }

        public Inventory Model => _model;

        public ICommand UnequipCommand => new DelegateCommand((param) => _model.UnEquip((InventoryItem)param));
    }
}
